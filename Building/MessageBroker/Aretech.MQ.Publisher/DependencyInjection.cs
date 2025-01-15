using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aretech.MQ.Publisher
{
	public static class DependencyInjection
	{
		public static void AddMQPublisher(this IServiceCollection services)
		{
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			});

			services.AddScoped<MQPublisherConfiguration>();
		}
	}
}
