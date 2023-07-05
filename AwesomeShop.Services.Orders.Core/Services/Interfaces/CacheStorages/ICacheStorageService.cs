namespace AwesomeShop.Services.Orders.Core.Services.Interfaces.CacheStorages;

public interface ICacheStorageService
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T data);
}