using Aretech.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aretech.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ApplicationMappingProfile).Assembly);
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			services.AddScoped<IMappingService, MappingService>();

			return services;
		}
	}
}