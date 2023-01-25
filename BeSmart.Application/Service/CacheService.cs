using System.Data.SqlTypes;
using System.Text.Json;
using BeSmart.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace BeSmart.Application.Service;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public string GetKey(string serviceName, string dataKey)
    {
        return $"{serviceName}-{dataKey}"
            .Replace("_", String.Empty)
            .ToLower();
    }

    public async Task CacheDataAsync<T>(string key, T data, TimeSpan? ttl = null)
    {
        var serializedData = JsonSerializer.Serialize(data);
        await _distributedCache.SetStringAsync(key,
            serializedData,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl ?? TimeSpan.FromHours(3)
            });
    }

    public async Task<T> GetCachedDataAsync<T>(string key) where T : class
    {
        var cachedData = await _distributedCache.GetAsync(key);

        if (cachedData is null)
        {
            return null;
        }

        var result = JsonSerializer.Deserialize<T>(cachedData);
        return result;
    }

    public async Task<bool> CachedDataExists(string key)
    {
        return await _distributedCache.GetAsync(key) != null;
    }

    public async Task DeleteCachedData(string key)
    {
        var isDataExists = await CachedDataExists(key);

        if (!isDataExists)
        {
            return;
        }
        
        await _distributedCache.RemoveAsync(key);
    }
}