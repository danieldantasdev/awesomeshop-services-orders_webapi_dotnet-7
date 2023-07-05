using AwesomeShop.Services.Orders.Core.Services.Interfaces.CacheStorages;
using AwesomeShop.Services.Orders.Infrastructure.Services.Implementations.CacheStorages;

namespace AwesomeShop.Services.Orders.Api.Extensions;

public static class CacheStorageExtension
{
    public static IServiceCollection AddRedisExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = "OrdersCache";
            options.Configuration = "localhost:6379";
        });

        serviceCollection.AddTransient<ICacheStorageService, CacheStorageService>();
        return serviceCollection;
    }
}