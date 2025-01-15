using Aretech.Caching.Abstract;
using Aretech.Caching.Redis.Abstract;
using Aretech.Caching.Redis.Serializers;
using Aretech.Caching.Redis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;

namespace Aretech.Caching.Redis
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddRedis(this IServiceCollection services)
		{
			var serviceProvider = services.BuildServiceProvider();
			var configuration = serviceProvider.GetRequiredService<IConfiguration>();

			var keyPrefix = configuration["AretechRedisCacheConfigurationApplicationName"] + "."
									 + configuration["AretechRedisCacheConfigurationPrefix"] + "."
									 + configuration["AretechRedisCacheConfigurationEnvironment"];


			services.AddStackExchangeRedisExtensions<CacheSerializer>(options =>
			{
				return new List<RedisConfiguration> { new RedisConfiguration()
				{
					ConnectionString = configuration.GetValue<string>("AretechRedisCacheConfigurationConnectionString"),
					User = configuration.GetValue<string>("AretechRedisCacheConfigurationUserName"),
					Password = configuration.GetValue<string>("AretechRedisCacheConfigurationPassword"),
					KeyPrefix= keyPrefix,
					Ssl= configuration.GetValue<bool>("AretechRedisCacheConfigurationSslEnabled",false),
					AbortOnConnectFail = configuration.GetValue<bool>("AretechRedisCacheConfigurationAbortConnect",false),
					Name = configuration.GetValue<string>("AretechRedisCacheConfigurationApplicationName"),
				}
				};
			});

			services.AddMemoryCache();

			services.AddSingleton<IDatabaseCacheService, DatabaseCacheServices>();
			services.AddSingleton<IObjectCacheService, ObjectCacheService>();
			services.AddSingleton<IRequestCacheService, RequestCacheService>();
			services.AddSingleton<IRedisConfiguration, Concrete.RedisConfiguration>();

			return services;
		}
	}
}