namespace Aretech.MQ.RabbitMQ.Concrete
{
	public sealed class MQDefault
	{
		public static int MessagesTTL { get; set; } = 1000 * 60 * 60 * 2;
	}
}