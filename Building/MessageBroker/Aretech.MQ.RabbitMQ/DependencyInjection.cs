using Aretech.MQ.RabbitMQ.Abstract;
using Aretech.MQ.RabbitMQ.Concrete;
using Aretech.MQ.RabbitMQ.Logging;
using Aretech.MQ.RabbitMQ.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aretech.MQ.RabbitMQ
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
		{
			services.AddSingleton<IObjectConvertFormat, ObjectConvertFormat>();
			services.AddSingleton<MQService>();
			services.AddSingleton<IPublisherService, PublisherService>();
			services.AddSingleton<IConsumerService, ConsumerService>();


			services.AddSingleton<MongoDbService>();
			services.AddSingleton<RabbitMQLoggingConfiguration>();
			services.AddSingleton<ICrytopgraphyService, CrytopgraphyService>();
			services.AddSingleton<IPropertyCryptionService, PropertyCryptionService>();


			return services;
		}
	}
}
