namespace Aretech.MQ.RabbitMQ.Wrappers
{
	public interface IConsumerWrapper
	{
		Task StartAsync(string message);
		Task EndAsync(string message);
	}
}