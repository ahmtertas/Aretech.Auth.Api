using Aretech.MQ.RabbitMQ.Abstract;
using Aretech.MQ.RabbitMQ.Concrete;
using Aretech.MQ.RabbitMQ.Logging;
using Aretech.MQ.RabbitMQ.Logging.Models;
using Aretech.MQ.RabbitMQ.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Aretech.MQ.RabbitMQ.Services
{
	public class PublisherService : IPublisherService
	{
		private readonly MQService _rabbitMQService;
		private readonly IObjectConvertFormat _objectConvertFormat;
		private readonly IConfiguration _configuration;
		private const int maxRetryAttempts = 5;
		private readonly MongoDbService _loggingDbService;
		private readonly RabbitMQLoggingConfiguration _loggingConfiguration;
		public PublisherService(IObjectConvertFormat objectConvertFormat, IConfiguration configuration, IServiceProvider serviceProvider)
		{
			_objectConvertFormat = objectConvertFormat;
			_configuration = configuration;
			_loggingConfiguration = serviceProvider.GetRequiredService<RabbitMQLoggingConfiguration>();
			_loggingDbService = serviceProvider.GetRequiredService<MongoDbService>();
			_rabbitMQService = serviceProvider.GetRequiredService<MQService>();
		}

		public void SendExchange(string message, string prefix, string exchangeName, string exchangeType, IPublisherWrapper? wrapper = null)
		{
			var rabbitMQConfiguration = new RabbitMQConfiguration(_configuration, prefix);

			if (!rabbitMQConfiguration.Enabled)
				return;

			if (rabbitMQConfiguration.OsUserPrefixEnabled)
				exchangeName = $"{Environment.UserName}-{exchangeName}";

			if (wrapper is not null)
				Task.Run(() => wrapper.StartAsync(message));

			var body = Encoding.UTF8.GetBytes(message);

			var correlationId = Guid.NewGuid().ToString();
			int retryAttempts = 0;

			while (retryAttempts < maxRetryAttempts)
			{
				try
				{
					var connection = _rabbitMQService.GetConnection(rabbitMQConfiguration, "Publisher");
					using (var channel = connection.CreateModel())
					{
						channel.ExchangeDeclare(exchangeName, exchangeType, durable: true, autoDelete: false, arguments: null);
						var properties = channel.CreateBasicProperties();
						properties.Persistent = true;
						properties.Expiration = MQDefault.MessagesTTL.ToString();
						properties.CorrelationId = correlationId;

						channel.BasicPublish(exchange: exchangeName,
											 routingKey: string.Empty,
											 mandatory: false,
											 basicProperties: properties,
											 body: body);

						if (wrapper is not null)
							Task.Run(() => wrapper.EndAsync(message));

						if (_loggingConfiguration.LoggingEnabled)
						{
							_loggingDbService.AddAsync(new MQMessage()
							{
								Id = Guid.NewGuid(),
								CorrelationId = correlationId,
								PublishDate = DateTime.Now,
								PublisherApplicationName = rabbitMQConfiguration.ApplicationName,
								PublisherPrefix = rabbitMQConfiguration.Prefix,
								PublisherType = PublisherType.Exchange,
								QueueName = exchangeName,
								Message = message
							}).GetAwaiter().GetResult();
						}
					}

					return;

				}
				catch (Exception ex)
				{
					retryAttempts++;
					var delay = TimeSpan.FromSeconds(Math.Pow(2, retryAttempts));
					if (retryAttempts >= maxRetryAttempts)
					{
						if (_loggingConfiguration.LoggingEnabled)
						{
							_loggingDbService.AddAsync(new MQQueueFailed()
							{
								Id = Guid.NewGuid(),
								CorelationId = correlationId,
								CreatedDate = DateTime.Now,
								ApplicationName = rabbitMQConfiguration.ApplicationName,
								Prefix = rabbitMQConfiguration.Prefix,
								HostName = rabbitMQConfiguration.HostName,
								Password = rabbitMQConfiguration.Password,
								Port = rabbitMQConfiguration.Port,
								SslEnabled = rabbitMQConfiguration.SslEnabled,
								UserName = rabbitMQConfiguration.UserName,
								Message = message,
								QueueName = exchangeName,
								PublisherType = PublisherType.Exchange,

							}).GetAwaiter().GetResult();
						}

						throw new Exception(ex.InnerException?.Message.ToString());
					}

					Thread.Sleep(delay);
				}
			}
		}

		public void SendExchange<T>(T model, string prefix, string exchangeName, string exchangeType, IPublisherWrapper? wrapper = null) where T : class, new()
		{
			var body = _objectConvertFormat.ObjectToJson(model);
			SendExchange(message: body, prefix: prefix, exchangeName: exchangeName, exchangeType: exchangeType, wrapper: wrapper);
		}

		public void SendQueue(string message, string prefix, string queueName, IPublisherWrapper? wrapper = null)
		{
			var rabbitMQConfiguration = new RabbitMQConfiguration(_configuration, prefix);

			if (!rabbitMQConfiguration.Enabled)
				return;

			if (rabbitMQConfiguration.OsUserPrefixEnabled)
				queueName = $"{Environment.UserName}-{queueName}";

			if (wrapper is not null)
				Task.Run(() => wrapper.StartAsync(message));

			var body = Encoding.UTF8.GetBytes(message);

			int retryAttempts = 0;
			var correlationId = Guid.NewGuid().ToString();

			while (retryAttempts < maxRetryAttempts)
			{
				try
				{
					var connection = _rabbitMQService.GetConnection(rabbitMQConfiguration, "Publisher");
					using (var channel = connection.CreateModel())
					{
						channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

						var properties = channel.CreateBasicProperties();
						properties.Persistent = true;
						properties.Expiration = MQDefault.MessagesTTL.ToString();
						properties.CorrelationId = correlationId;

						channel.BasicPublish(exchange: string.Empty,
											 routingKey: queueName,
											 mandatory: false,
											 basicProperties: properties,
											 body: body);


						if (wrapper is not null)
							Task.Run(() => wrapper.EndAsync(message));

						if (_loggingConfiguration.LoggingEnabled)
						{
							_loggingDbService.AddAsync(new MQMessage()
							{
								Id = Guid.NewGuid(),
								CorrelationId = correlationId,
								PublishDate = DateTime.Now,
								PublisherApplicationName = rabbitMQConfiguration.ApplicationName,
								PublisherPrefix = rabbitMQConfiguration.Prefix,
								Message = message,
								QueueName = queueName,
								PublisherType = PublisherType.Queue
							}).GetAwaiter().GetResult();
						}
					}

					return;
				}
				catch (Exception ex)
				{
					retryAttempts++;
					var delay = TimeSpan.FromSeconds(2);

					if (retryAttempts >= maxRetryAttempts)
					{
						if (_loggingConfiguration.LoggingEnabled)
						{
							_loggingDbService.AddAsync(new MQQueueFailed()
							{
								Id = Guid.NewGuid(),
								CorelationId = correlationId,
								CreatedDate = DateTime.Now,
								ApplicationName = rabbitMQConfiguration.ApplicationName,
								HostName = rabbitMQConfiguration.HostName,
								Message = message,
								QueueName = queueName,
								PublisherType = PublisherType.Queue,
								Password = rabbitMQConfiguration.Password,
								Port = rabbitMQConfiguration.Port,
								Prefix = rabbitMQConfiguration.Prefix,
								SslEnabled = rabbitMQConfiguration.SslEnabled,
								UserName = rabbitMQConfiguration.UserName,
							}).GetAwaiter().GetResult();
						}

						throw new Exception(ex.InnerException?.Message.ToString());
					}

					Thread.Sleep(delay);
				}
			}
		}

		public void SendQueue<T>(T model, string prefix, string queueName, IPublisherWrapper? wrapper = null) where T : class, new()
		{
			var body = _objectConvertFormat.ObjectToJson(model);
			SendQueue(message: body, prefix: prefix, queueName: queueName, wrapper: wrapper);
		}
	}
}