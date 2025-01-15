using Aretech.Caching.Abstract;
using Aretech.Caching.Redis.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Aretech.Caching.Redis.Services
{
	public sealed class RequestCacheService : IRequestCacheService
	{
		private readonly IRedisConfiguration _redisConfiguration;
		private readonly IRedisDatabase _cache;

		public RedisDatabase db => RedisDatabase.DB1;

		public RequestCacheService(IRedisConfiguration redisConfiguration, IRedisClient redisClient)
		{
			_redisConfiguration = redisConfiguration;
			_cache = redisClient.GetDb((int)db);
		}


		public async Task<T?> GetAsync<T>(string key)
		{
			if (!_redisConfiguration.Enabled)
				return default(T?);

			return await _cache.GetAsync<T>(key);
		}


		public async Task<bool> SetAsync<T>(string key, T value, DistributedCacheEntryOptions options)
		{
			if (!_redisConfiguration.Enabled)
				return true;

			TimeSpan expiry = default(TimeSpan);

			if (options.AbsoluteExpirationRelativeToNow.HasValue)
				expiry = (TimeSpan)options.AbsoluteExpirationRelativeToNow;

			if (options.SlidingExpiration.HasValue)
				throw new NotSupportedException("SlidingExpiration is not supported yet");

			if (options.AbsoluteExpiration.HasValue)
				throw new NotSupportedException("AbsoluteExpiration is not supported yet");

			return await _cache.AddAsync<T>(key, value, expiry);
		}


		public async Task<bool> SetAsync<T>(string key, T value, int? cacheTime = null)
		{
			if (!_redisConfiguration.Enabled)
				return true;
			TimeSpan expiry = cacheTime.HasValue ? TimeSpan.FromMinutes(cacheTime.Value) :
												   TimeSpan.FromMinutes(_redisConfiguration.DefaultCacheTime);

			return await _cache.AddAsync(key, value, expiry);
		}

		public async Task<bool> ExistsAsync(string key)
		{
			if (!_redisConfiguration.Enabled)
				return false;

			return await _cache.ExistsAsync(key);
		}


		public async Task<bool> DeleteAsync(string key)
		{
			if (!_redisConfiguration.Enabled)
				return false;

			return await _cache.RemoveAsync(key);
		}

		public async Task<long> AllDeleteAsync(string pattern)
		{
			if (!_redisConfiguration.Enabled)
				return long.MinValue;

			IEnumerable<string> keys = await _cache.SearchKeysAsync(pattern);

			return await _cache.RemoveAllAsync(keys.ToArray());
		}

		public async Task FlushAsync()
		{
			if (!_redisConfiguration.Enabled)
				return;

			await _cache.FlushDbAsync();
		}
	}
}
