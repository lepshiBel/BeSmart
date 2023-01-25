using System.Data.SqlTypes;
using Microsoft.Extensions.Caching.Distributed;

namespace BeSmart.Domain.Interfaces;

public interface ICacheService
{
    public string GetKey(string serviceName, string dataKey);

    public Task CacheDataAsync<T>(string key, T data, TimeSpan? ttl = null);

    public Task<T> GetCachedDataAsync<T>(string key) where T : class;

    public Task<bool> CachedDataExists(string key);

    public Task DeleteCachedData(string key);
}