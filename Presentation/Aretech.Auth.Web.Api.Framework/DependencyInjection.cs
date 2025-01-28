using Aretech.Application;
using Aretech.Auth.Web.Api.Framework.Middlewares;
using Aretech.Auth.Web.Api.Framework.Models;
using Aretech.Auth.Web.Api.Framework.Serilog;
using Aretech.Caching.Redis;
using Aretech.Core;
using Aretech.Infrastructure;
using Aretech.Infrastructure.Data.EfCore.PostgreSQL;
using Aretech.MQ.Publisher;
using Aretech.MQ.RabbitMQ;
using Aretech.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;
using Serilog.Debugging;
using Serilog.Enrichers.WithCaller;
using System.Text;


namespace Aretech.Auth.Web.Api.Framework
{
	public static class DependencyInjection
	{
		public static void StartApplication(this WebApplicationBuilder builder)
		{

			builder.Services.AddControllers();

			builder.Services.AddHttpContextAccessor();

			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Aretech.Auth.Api", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Jwt Auth header. 'Bearer {token}'"
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[]{ }
					}
				});
			});

			builder.Services.AddRedis();
			builder.Services.AddRabbitMQ();
			builder.Services.AddEfCorePostgreSQL();
			builder.Services.AddInfrastructure();
			builder.Services.AddApplication();
			builder.Services.AddServices();
			builder.Services.AddMQPublisher();
			builder.Services.AddScoped<IAuthenticationContext, AuthenticationContext>();

			var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("CorsSettings", policy =>
				{
					policy.WithOrigins(allowedOrigins)
							.AllowAnyOrigin()
							.AllowAnyHeader();
				});
			});

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
		   .AddJwtBearer(options =>
		   {
			   options.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidateIssuer = true,
				   ValidateAudience = true,
				   ValidateIssuerSigningKey = true,
				   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
				   ValidIssuer = builder.Configuration["Jwt:Issuer"],
				   ValidAudience = builder.Configuration["Jwt:Audience"],
				   ClockSkew = TimeSpan.Zero, // Süre farkını önlemek için sıfırlanabilir,
				   ValidateLifetime = true
			   };

			   options.Events = new JwtBearerEvents
			   {
				   OnAuthenticationFailed = context =>
				   {
					   Console.WriteLine("Authentication failed: " + context.Exception.Message);
					   return Task.CompletedTask;
				   },
				   OnTokenValidated = context =>
				   {
					   Console.WriteLine("Token validated for: " + context.Principal.Identity.Name);
					   return Task.CompletedTask;
				   }
			   };
		   });

			var serviceProvider = builder.Services.BuildServiceProvider();
			var configuration = serviceProvider.GetRequiredService<IConfiguration>();

			var rabbitMQOperationConnectionString = string.Empty;

			if (!string.IsNullOrEmpty(configuration.GetValue<string>("RabbitMQOperationConnectionStringUrl")))
				rabbitMQOperationConnectionString = configuration.GetValue<string>("RabbitMQOperationConnectionStringUrl");
			else
				rabbitMQOperationConnectionString = $"amqp://{configuration.GetValue<string>("RabbitMQOperationConnectionStringUserName")}:{configuration.GetValue<string>("RabbitMQOperationConnectionStringPassword")}" +
													$"@{configuration.GetValue<string>("RabbitMQOperationConnectionStringHostName")}:{configuration.GetValue<string>("RabbitMQOperationConnectionStringPort")}";


			builder.Services.AddHealthChecks()
				.AddNpgSql(
				connectionString: configuration["PgSqlDbConnectionString"],
				name: "",
				failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
				tags: new[] { "db", "sql", "postgresql" })
				//.AddMongoDb(
				//mongodbConnectionString: configuration[""],
				//name: "mongodb-rabbitmqlog",
				//failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
				//tags: new[] { "db", "nosql", "mongodb" }

				//)
				.AddRabbitMQ(
				rabbitConnectionString: rabbitMQOperationConnectionString,
				name: "rabbitmq-operation",
				failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
				timeout: TimeSpan.FromSeconds(5),
				tags: new string[] { "ready", "alive" });
			//.AddRedis(
			//redisConnectionString: configuration[""],
			//name: "redis-main",
			//failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
			//timeout: TimeSpan.FromSeconds(5),
			//tags: new string[] { "ready", "alive" });


			Log.Logger = new LoggerConfiguration()
						 .MinimumLevel.Information()
						 .WriteTo.Console()
						 .Enrich.FromLogContext()
						 .Enrich.WithMachineName()
						 .Enrich.WithThreadId()
						 .Enrich.WithCaller()
						 .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
						 .Enrich.WithProperty("Application", "Aretech.Auth.Api")
						 .Enrich.With<ExceptionTypeEnricher>()
						 .CreateLogger();

			builder.Host.UseSerilog();
			SelfLog.Enable(Console.Out);


			Configure(builder);
		}

		public static void Configure(WebApplicationBuilder builder)
		{
			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("CorsSettings");

			app.UseAuthentication();
			app.UseAuthorization();


			var rateLimitingRules = builder.Configuration.GetSection("IpRateLimiting:GeneralRules").Get<List<RateLimitingRule>>();
			app.UseMiddleware<RateLimitingMiddleware>(rateLimitingRules);
			app.UseMiddleware<ExceptionHandlerMiddleware>();

			app.MapControllers();

			app.MapHealthChecks("/health");

			app.UseMetricServer("/metrics");

			app.Run();
		}
	}
}