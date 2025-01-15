namespace Aretech.MQ.RabbitMQ.Wrappers
{
	public interface IPublisherWrapper
	{
		Task StartAsync(string message);
		Task EndAsync(string message);
	}
}