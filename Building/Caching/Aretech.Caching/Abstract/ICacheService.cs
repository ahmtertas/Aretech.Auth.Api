namespace Aretech.Caching.Abstract
{
	public interface ICacheService
	{
		// String veri türü için metodlar
		Task<string> GetStringAsync(string cacheKey);
		Task SetStringAsync(string cacheKey, string value, TimeSpan? expirationTime = null);
		Task<bool> RemoveAsync(string cacheKey);

		// Nesne (object) veri türü için metodlar
		Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem, TimeSpan? expirationTime = null);
		Task SetAsync<T>(string cacheKey, T value, TimeSpan? expirationTime = null);
		Task<T> GetAsync<T>(string cacheKey);

		// Koleksiyon (hash) veri türü için metodlar
		Task<IDictionary<string, string>> GetHashAsync(string cacheKey);
		Task SetHashAsync(string cacheKey, IDictionary<string, string> values, TimeSpan? expirationTime = null);
		Task<bool> RemoveHashFieldAsync(string cacheKey, string field);

		// Önbellek temizleme metodları
		Task ClearCacheAsync();
		Task ClearCacheByPrefixAsync(string prefix);
	}
}