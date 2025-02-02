using Aretech.Caching.Abstract;
using Aretech.Caching.Redis.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Aretech.Caching.Redis
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddRedis(this IServiceCollection services)
		{
			services.AddScoped<ICacheService, RedisCacheService>();
			return services;
		}
	}
}