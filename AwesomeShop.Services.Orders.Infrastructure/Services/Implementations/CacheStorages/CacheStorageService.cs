using AwesomeShop.Services.Orders.Core.Services.Interfaces.CacheStorages;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AwesomeShop.Services.Orders.Infrastructure.Services.Implementations.CacheStorages;

public class CacheStorageService : ICacheStorageService
{
    private readonly IDistributedCache _distributedCache;

    public CacheStorageService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var objectString = await _distributedCache.GetStringAsync(key);
        if (string.IsNullOrWhiteSpace(objectString))
        {
            Console.WriteLine($"cache key {key} not found!");
            return default(T)!;
        }
        
        Console.WriteLine($"cache key found: {key}");

        return JsonConvert.DeserializeObject<T>(objectString)!;
    }

    public async Task SetAsync<T>(string key, T data)
    {
        var memoryCacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
            SlidingExpiration = TimeSpan.FromSeconds(1200)
        };

        var objectString = JsonConvert.SerializeObject(data);
        Console.WriteLine($"cache set for key: {key}");
        
        await _distributedCache.SetStringAsync(key, objectString, memoryCacheEntryOptions);
    }
}