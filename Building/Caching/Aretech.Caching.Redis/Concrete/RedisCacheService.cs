using Aretech.Caching.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace Aretech.Caching.Redis.Concrete
{
	public class RedisCacheService : ICacheService
	{
		private readonly IDistributedCache _distributedCache;

		public RedisCacheService(IDistributedCache distributedCache)
		{
			_distributedCache = distributedCache;
		}

		public async Task<string> GetStringAsync(string cacheKey)
		{
			return await _distributedCache.GetStringAsync(cacheKey);
		}

		public async Task SetStringAsync(string cacheKey, string value, TimeSpan? expirationTime = null)
		{
			var options = new DistributedCacheEntryOptions();
			if (expirationTime.HasValue)
			{
				options.AbsoluteExpirationRelativeToNow = expirationTime;
			}

			await _distributedCache.SetStringAsync(cacheKey, value, options);
		}

		// Nesne (object) veri türü için metodlar
		public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem, TimeSpan? expirationTime = null)
		{
			var cachedData = await GetAsync<T>(cacheKey);
			if (cachedData != null)
			{
				return cachedData;
			}

			var data = await createItem();
			await SetAsync(cacheKey, data, expirationTime);
			return data;
		}

		public async Task SetAsync<T>(string cacheKey, T value, TimeSpan? expirationTime = null)
		{
			var serializedData = JsonSerializer.Serialize(value);
			await SetStringAsync(cacheKey, serializedData, expirationTime);
		}

		public async Task<T> GetAsync<T>(string cacheKey)
		{
			var cachedData = await GetStringAsync(cacheKey);
			if (cachedData == null)
			{
				return default;
			}

			return JsonSerializer.Deserialize<T>(cachedData);
		}

		public async Task<IDictionary<string, string>> GetHashAsync(string cacheKey)
		{
			var redis = await GetRedisDatabaseAsync();
			var hashEntries = await redis.HashGetAllAsync(cacheKey);
			return hashEntries.ToStringDictionary();
		}

		public async Task SetHashAsync(string cacheKey, IDictionary<string, string> values, TimeSpan? expirationTime = null)
		{

			var redis = await GetRedisDatabaseAsync();
			var hashEntries = values.Select(kv => new HashEntry(kv.Key, kv.Value)).ToArray();
			await redis.HashSetAsync(cacheKey, hashEntries);

			if (expirationTime.HasValue)
			{
				await redis.KeyExpireAsync(cacheKey, expirationTime);
			}
		}

		public async Task<bool> RemoveHashFieldAsync(string cacheKey, string field)
		{
			var redis = await GetRedisDatabaseAsync();
			return await redis.HashDeleteAsync(cacheKey, field);
		}

		public async Task ClearCacheAsync()
		{
			var redis = await GetRedisDatabaseAsync();
			var server = redis.Multiplexer.GetServer(redis.Multiplexer.GetEndPoints().First());
			await server.FlushDatabaseAsync();
		}

		public async Task ClearCacheByPrefixAsync(string prefix)
		{

			var redis = await GetRedisDatabaseAsync();
			var server = redis.Multiplexer.GetServer(redis.Multiplexer.GetEndPoints().First());
			var keys = server.Keys(pattern: $"{prefix}*").ToArray();

			if (keys.Any())
			{
				await redis.KeyDeleteAsync(keys);
			}
		}

		private async Task<IDatabase> GetRedisDatabaseAsync()
		{
			var redis = ConnectionMultiplexer.Connect("localhost:6379");
			return redis.GetDatabase();
		}

		public async Task<bool> RemoveAsync(string cacheKey)
		{
			await _distributedCache.RemoveAsync(cacheKey);
			return true;
		}
	}
}