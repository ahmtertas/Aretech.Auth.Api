using RabbitMQ.Client;
using System.Net.Security;

namespace Aretech.MQ.RabbitMQ.Concrete
{
	internal sealed class MQService
	{
		private static IConnection _connection;
		private static readonly object _lock = new object();

		public IConnection GetConnection(RabbitMQConfiguration configuration, string instance)
		{
			try
			{
				if (_connection is null || !_connection.IsOpen)
				{
					lock (_lock)
					{
						if (_connection == null || !_connection.IsOpen)
						{
							var factory = new ConnectionFactory
							{
								AutomaticRecoveryEnabled = true,
								NetworkRecoveryInterval = TimeSpan.FromSeconds(5),
								RequestedHeartbeat = TimeSpan.FromSeconds(30),
								TopologyRecoveryEnabled = true,
								DispatchConsumersAsync = false
							};

							var applicationName = $"{configuration.ApplicationName}-{configuration.Prefix}-{configuration.Environment}-{instance}";

							factory.ClientProperties = new Dictionary<string, object>
							{
								{ "ApplicationName", configuration.ApplicationName },
								{"Environment", configuration.Environment },
								{"Prefix", configuration.Prefix },
								{"Instance", instance },
							};

							if (string.IsNullOrEmpty(configuration.Url))
							{
								factory.HostName = configuration.HostName;
								factory.Port = configuration.Port;
								factory.UserName = configuration.UserName;
								factory.Password = configuration.Password;
							}
							else
								factory.Uri = new Uri(configuration.Url);

							if (configuration.SslEnabled)
							{
								factory.Ssl = new SslOption
								{
									Enabled = true,
									AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch | SslPolicyErrors.RemoteCertificateChainErrors
								};
							}

							_connection = factory.CreateConnection(applicationName);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message.ToString());
			}

			return _connection;
		}


	}
}
