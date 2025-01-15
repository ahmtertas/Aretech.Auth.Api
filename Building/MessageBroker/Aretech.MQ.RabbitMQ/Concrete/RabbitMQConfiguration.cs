using Microsoft.Extensions.Configuration;

namespace Aretech.MQ.RabbitMQ.Concrete
{
	internal sealed class RabbitMQConfiguration
	{
		public IConfiguration _configuration;
		public string Prefix { get; set; }
		public RabbitMQConfiguration(IConfiguration configuration, string prefix)
		{
			_configuration = configuration;
			Prefix = prefix;
		}

		public string Environment => _configuration[$"RabbitMQ{Prefix}ConfigurationEnvironment"]
										  ?? throw new ArgumentException("RabbitMQ.Configuration.Environment");

		public string ApplicationName => _configuration[$"RabbitMQ{Prefix}ConfigurationApplicationName"]
										  ?? throw new ArgumentException("RabbitMQ.Configuration.ApplicationName");

		public string HostName => _configuration[$"RabbitMQ{Prefix}ConfigurationHostName"]
										  ?? throw new ArgumentException("RabbitMQ.Configuration.HostName");

		public int Port => Convert.ToInt32(_configuration[$"RabbitMQ{Prefix}ConfigurationPort"]
										  ?? throw new ArgumentException("RabbitMQ.Configuration.Port"));

		public string UserName => _configuration[$"RabbitMQ{Prefix}ConfigurationUserName"]
										  ?? throw new ArgumentException("RabbitMQ.Configuration.UserName");

		public string Password => _configuration[$"RabbitMQ{Prefix}ConfigurationPassword"]
										  ?? throw new ArgumentException("RabbitMQ.Configuration.Password");


		public string Url => _configuration[$"RabbitMQ{Prefix}ConfigurationUrl"] ?? string.Empty;

		public bool SslEnabled
		{
			get
			{
				var sslEnabledValue = _configuration[$"RabbitMQ{Prefix}ConfigurationSslEnabled"];
				if (string.IsNullOrEmpty(sslEnabledValue))
					return false;
				bool.TryParse(sslEnabledValue, out bool sslEnabled);
				return sslEnabled;
			}
		}

		public bool Enabled
		{
			get
			{
				var enabledValue = _configuration[$"RabbitMQ{Prefix}ConfigurationEnabled"];
				if (string.IsNullOrEmpty(enabledValue))
					return false;

				bool.TryParse(enabledValue, out bool enabled);
				return enabled;
			}
		}

		public bool OsUserPrefixEnabled
		{
			get
			{
				var enabledValue = _configuration[$"RabbitMQ{Prefix}ConfigurationPrefixEnabled"];
				if (string.IsNullOrEmpty(enabledValue))
					return false;

				bool.TryParse(enabledValue, out bool enabled);
				return enabled;
			}
		}
	}
}
