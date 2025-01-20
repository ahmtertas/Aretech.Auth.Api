using Aretech.Infrastructure.Data.EfCore.PostgreSQL.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddEfCorePostgreSQL(this IServiceCollection services)
		{
			var serviceProvider = services.BuildServiceProvider();
			var configuration = serviceProvider.GetRequiredService<IConfiguration>();

			services.AddDbContext<AretechDbContext>(options => options.UseNpgsql(configuration["PgSqlDbConnectionString"]));
			services.AddScoped(typeof(IRepository<>), typeof(AretechEFRepository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IHashService, HashService>();

			return services;
		}
	}
}