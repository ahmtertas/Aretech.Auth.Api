namespace Aretech.MQ.RabbitMQ.Logging.Models
{
	class MQError
	{
		public Guid Id { get; set; }
		public string CorrelationId { get; set; }
		public string? PublisherPrefix { get; set; }
		public string? PublisherApplicationName { get; set; }
		public string? ConsumerPrefix { get; set; }
		public string? ConsumerApplicationName { get; set; }
		public string ErrorMessage { get; set; } = null!;
		public DateTime ErrorDate { get; set; }
	}
}
