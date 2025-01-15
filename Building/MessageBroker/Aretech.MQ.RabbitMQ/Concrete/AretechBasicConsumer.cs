using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Aretech.MQ.RabbitMQ.Concrete
{
	public class AretechBasicConsumer : IBasicConsumer
	{

		private readonly IModel _channel;

		public AretechBasicConsumer(IModel channel)
		{
			_channel = channel;
		}

		public string ConsumerTag { get; private set; }
		public IModel Model => throw new NotImplementedException();

		public event EventHandler<ConsumerEventArgs> ConsumerCancelled;
		public event EventHandler<BasicDeliverEventArgs> OnMessageReceived;
		public void HandleBasicCancel(string consumerTag)
		{
			throw new NotImplementedException();
		}

		public void HandleBasicCancelOk(string consumerTag)
		{
			throw new NotImplementedException();
		}

		public void HandleBasicConsumeOk(string consumerTag)
		{
			throw new NotImplementedException();
		}


		public void HandleModelShutdown(object model, ShutdownEventArgs reason)
		{
			throw new NotImplementedException();
		}

		public void HandleBasicDeliver(string consumerTag,
									   ulong deliveryTag,
									   bool redelivered,
									   string exchange,
									   string routingKey,
									   IBasicProperties properties,
									   ReadOnlyMemory<byte> body)
		{
			var ea = new BasicDeliverEventArgs
			{
				ConsumerTag = consumerTag,
				DeliveryTag = deliveryTag,
				Redelivered = redelivered,
				Exchange = exchange,
				RoutingKey = routingKey,
				Body = body
			};

			OnMessageReceived?.Invoke(this, ea);
		}

		public void HandleBasicDeliver(string consumerTag,
									   ulong deliveryTag,
									   bool redelivered,
									   string exchange,
									   string routingKey,
									   IBasicProperties properties,
									   byte[] body)
		{
			var ea = new BasicDeliverEventArgs
			{
				ConsumerTag = consumerTag,
				DeliveryTag = deliveryTag,
				Redelivered = redelivered,
				Exchange = exchange,
				RoutingKey = routingKey,
				Body = body
			};

			OnMessageReceived?.Invoke(this, ea);
		}

	}
}
