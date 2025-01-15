using Aretech.MQ.RabbitMQ.Concrete;
using Aretech.MQ.RabbitMQ.Logging;
using Aretech.MQ.RabbitMQ.Logging.Models;
using Aretech.MQ.RabbitMQ.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Aretech.MQ.RabbitMQ.Services
{
	public sealed class ConsumerService : IConsumerService
	{
		private readonly MQService _rabbitMQService;
		private readonly IConfiguration _configuration;
		private readonly MongoDbService _logDbService;
		private readonly RabbitMQLoggingConfiguration _loggingConfiguration;
		public ConsumerService(IConfiguration configuration, IServiceProvider serviceProvider)
		{
			_configuration = configuration;
			_loggingConfiguration = serviceProvider.GetRequiredService<RabbitMQLoggingConfiguration>();
			_logDbService = serviceProvider.GetRequiredService<MongoDbService>();
			_rabbitMQService = serviceProvider.GetRequiredService<MQService>();
		}
		public void StartConsumerAckAsync(string queueName,
										  string prefix,
										  Func<string, BasicDeliverEventArgs, IModel, CancellationToken, Task> processMessage,
										  IConsumerWrapper? wrapper = null)
		{
			var rabbitMQConfiguration = new RabbitMQConfiguration(configuration: _configuration, prefix: prefix);

			if (!rabbitMQConfiguration.Enabled)
				return;

			if (rabbitMQConfiguration.OsUserPrefixEnabled)
				queueName = $"{Environment.UserName}-{queueName}";

			var connection = _rabbitMQService.GetConnection(rabbitMQConfiguration, "Consumer");
			var channel = connection.CreateModel();
			channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += (model, ea) =>
			{
				var correlationId = ea.BasicProperties.CorrelationId;

				using var cts = new CancellationTokenSource();
				var token = cts.Token;

				try
				{
					var body = ea.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);

					if (wrapper is not null)
						Task.Run(() => wrapper.StartAsync(message: message));

					processMessage(message, ea, channel, default);

					if (wrapper is not null)
						Task.Run(() => wrapper.EndAsync(message: message));

					if (_loggingConfiguration.LoggingEnabled)
					{
						try
						{
							var messageLog = _logDbService.GetByCorrelationId<MQMessage>(correlationId).GetAwaiter().GetResult();
							messageLog.ConsumerPrefix = rabbitMQConfiguration.Prefix;
							messageLog.ConsumerApplicationName = rabbitMQConfiguration.ApplicationName;
							messageLog.ConsumeDate = DateTime.Now;
							_logDbService.UpdateByCorrelationId<MQMessage>(correlationId, messageLog).GetAwaiter().GetResult();
						}
						catch { }
					}
				}
				catch (Exception ex)
				{

					var errorMessage = "Exception Message = " + ex.Message + "\r\n StackTrace = " + ex.StackTrace;
					if (ex.InnerException != null)
						errorMessage += "\r\nInner Exception Message = " + ex.InnerException.Message + "\r\nInner StackTrace = " + ex.InnerException.StackTrace;

					if (_loggingConfiguration.LoggingEnabled)
					{
						try
						{
							var messageLog = _logDbService.GetByCorrelationId<MQMessage>(correlationId).GetAwaiter().GetResult();
							_logDbService.AddAsync(new MQError
							{
								Id = Guid.NewGuid(),
								ErrorMessage = errorMessage,
								ErrorDate = DateTime.Now,
								ConsumerPrefix = rabbitMQConfiguration.Prefix,
								ConsumerApplicationName = rabbitMQConfiguration.ApplicationName,
								CorrelationId = correlationId,
								PublisherApplicationName = rabbitMQConfiguration.ApplicationName,
								PublisherPrefix = rabbitMQConfiguration.Prefix
							}).GetAwaiter().GetResult();
						}
						catch { }
					}

					throw;
				}
			};

			channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
		}
	}
}