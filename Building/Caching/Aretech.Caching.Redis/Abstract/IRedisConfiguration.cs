namespace Aretech.Caching.Redis.Abstract
{
	public interface IRedisConfiguration
	{
		string ConnectionString { get; }
		string UserName { get; }
		string Password { get; }
		int DefaultCacheTime { get; }
		bool Enabled { get; }
		string ApplicationName { get; }
		string Prefix { get; }
		string Environment { get; }
	}
}