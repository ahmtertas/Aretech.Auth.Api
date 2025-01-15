using Aretech.MQ.RabbitMQ.Wrappers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Aretech.MQ.RabbitMQ.Services
{
	public interface IConsumerService
	{
		void StartConsumerAckAsync(string queueName, string prefix, Func<string, BasicDeliverEventArgs, IModel, CancellationToken, Task> processMessage, IConsumerWrapper? wrapper = null);
	}
}
