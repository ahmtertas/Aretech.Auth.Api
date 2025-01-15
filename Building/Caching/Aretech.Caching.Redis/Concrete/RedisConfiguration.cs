using Aretech.Caching.Redis.Abstract;
using Microsoft.Extensions.Configuration;

namespace Aretech.Caching.Redis.Concrete
{
	public sealed class RedisConfiguration : IRedisConfiguration
	{
		public IConfiguration _configuration { get; }

		public RedisConfiguration(IConfiguration configuration) => _configuration = configuration;

		public string ConnectionString => _configuration["AretechRedisCacheConfigurationConnectionString"]
									   ?? throw new ArgumentException("AretechRedisCacheConfigurationConnectionString");


		public string UserName => _configuration["AretechRedisCacheConfigurationUserName"] ?? string.Empty;

		public string Password => _configuration["AretechRedisCacheConfigurationPassword"] ?? string.Empty;

		public string ApplicationName => _configuration["AretechRedisCacheConfigurationApplicationName"] ?? string.Empty;

		public string Prefix => _configuration["AretechRedisCacheConfigurationPrefix"] ?? string.Empty;

		public string Environment => _configuration["AretechRedisCacheConfigurationEnvironment"] ?? string.Empty;


		public bool Enabled
		{
			get
			{
				var enabledValue = _configuration["AretechRedisCacheConfigurationEnabled"];
				if (string.IsNullOrEmpty(enabledValue))
					return false;
				bool.TryParse(enabledValue, out bool enabled);
				return enabled;
			}
		}

		public int DefaultCacheTime
		{
			get
			{
				var defaultCacheTimeValue = _configuration["AretechRedisCacheConfigurationDefaultCacheTime"];
				if (string.IsNullOrEmpty(defaultCacheTimeValue))
					throw new ArgumentException("AretechRedisCacheConfigurationDefaultCacheTime");


				if (int.TryParse(defaultCacheTimeValue, out int defaultCacheTime))
				{
					return defaultCacheTime;
				}

				throw new ArgumentException("AretechRedisCacheConfigurationDefaultCacheTime");
			}
		}
	}
}