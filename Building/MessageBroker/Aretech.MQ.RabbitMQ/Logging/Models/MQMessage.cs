namespace Aretech.MQ.RabbitMQ.Logging.Models
{
	class MQMessage
	{
		public Guid Id { get; set; }
		public string CorrelationId { get; set; } = null!;
		public string PublisherPrefix { get; set; } = null!;
		public string PublisherApplicationName { get; set; } = null!;
		public string? ConsumerPrefix { get; set; } = null!;
		public string? ConsumerApplicationName { get; set; } = null!;
		public string Message { get; set; } = null!;

		public DateTime PublishDate { get; set; }
		public DateTime? ConsumeDate { get; set; }
		public string QueueName { get; set; } = null!;
		public PublisherType PublisherType { get; set; }
	}
}
