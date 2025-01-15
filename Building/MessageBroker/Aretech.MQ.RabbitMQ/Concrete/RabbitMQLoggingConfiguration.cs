using Microsoft.Extensions.Configuration;

namespace Aretech.MQ.RabbitMQ.Concrete
{
	internal class RabbitMQLoggingConfiguration
	{
		public IConfiguration _configuration { get; }
		public RabbitMQLoggingConfiguration(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string LoggingConnectionString => _configuration.GetValue<string>("RabbitMQLoggingMongoDBConnectionString")
			?? throw new InvalidOperationException("RabbitMQLoggingMongoDBConnectionString");

		public string LoggingDatabaseName => _configuration.GetValue<string>("RabbitMQLoggingMongoDBDatabaseName")
			?? throw new InvalidOperationException("RabbitMQLoggingMongoDBDatabaseName");

		public string LoggingCollectionName => _configuration.GetValue<string>("RabbitMQLoggingMongoDBCollectionName")
			?? throw new InvalidOperationException("RabbitMQLoggingMongoDBCollectionName");

		public bool LoggingEnabled => _configuration.GetValue<bool>("RabbitMQLoggingMongoDBEnabled");
	}
}