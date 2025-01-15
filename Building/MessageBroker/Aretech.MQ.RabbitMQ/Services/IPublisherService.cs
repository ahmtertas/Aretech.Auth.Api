using Aretech.MQ.RabbitMQ.Wrappers;

namespace Aretech.MQ.RabbitMQ.Services
{
	public interface IPublisherService
	{
		void SendQueue<T>(T model, string prefix, string queueName, IPublisherWrapper? wrapper = null) where T : class, new();
		void SendExchange<T>(T model, string prefix, string exchangeName, string exchangeType, IPublisherWrapper? wrapper = null) where T : class, new();
		void SendQueue(string message, string prefix, string queueName, IPublisherWrapper? wrapper = null);
		void SendExchange(string message, string prefix, string exchangeName, string exchangeType, IPublisherWrapper? wrapper = null);

	}
}
