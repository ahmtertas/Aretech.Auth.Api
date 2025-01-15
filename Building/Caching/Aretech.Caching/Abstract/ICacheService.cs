using Microsoft.Extensions.Caching.Distributed;

namespace Aretech.Caching.Abstract
{
	public interface ICacheService
	{
		Task<bool> DeleteAsync(string key);
		Task<long> AllDeleteAsync(string pattern);
		Task<bool> ExistsAsync(string key);
		Task<T?> GetAsync<T>(string key);
		Task<bool> SetAsync<T>(string key, T value, DistributedCacheEntryOptions options);
		Task<bool> SetAsync<T>(string key, T value, int? cacheTime = null);
		Task FlushAsync();
	}
}