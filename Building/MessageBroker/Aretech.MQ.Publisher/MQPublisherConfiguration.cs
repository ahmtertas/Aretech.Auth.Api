
using Microsoft.Extensions.Configuration;


namespace Aretech.MQ.Publisher
{
	internal class MQPublisherConfiguration
	{
		public IConfiguration _configuration { get; }

		public MQPublisherConfiguration(IConfiguration configuration) => _configuration = configuration;

		public bool Enabled => _configuration.GetValue<bool>("MQPublishConfigurationEnabled");


		#region Account

		public bool AddAccountEnabled => _configuration.GetValue<bool>("MQPublishConfigurationAddAcountEnabled");
		public string AddAccountQueueName => _configuration.GetValue<string>("MQPublishConfigurationAddAcountQueueName")
											?? throw new InvalidOperationException("Queue name is not configured.");

		#endregion
	}
}
