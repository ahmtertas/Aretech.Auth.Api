namespace Aretech.MQ.RabbitMQ.Logging.Models
{
	internal class MQQueueFailed
	{
		public Guid Id { get; set; }
		public string CorelationId { get; set; } = null!;
		public string Prefix { get; set; } = null!;
		public string ApplicationName { get; set; } = null!;
		[IsCrypted]
		public string HostName { get; set; } = null!;
		[IsCrypted]
		public int Port { get; set; }
		[IsCrypted]
		public string UserName { get; set; } = null!;
		[IsCrypted]
		public string Password { get; set; } = null!;

		public bool SslEnabled { get; set; }
		public string Message { get; set; } = null!;
		public DateTime CreatedDate { get; set; }
		public string QueueName { get; set; } = null!;
		public PublisherType PublisherType { get; set; }
	}
}
