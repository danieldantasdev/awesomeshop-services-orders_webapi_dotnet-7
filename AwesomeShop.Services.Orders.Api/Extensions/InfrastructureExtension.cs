using AwesomeShop.Services.Orders.Core.Repositories;
using AwesomeShop.Services.Orders.Core.Repositories.Interfaces;
using AwesomeShop.Services.Orders.Infrastructure.Persistence.Repositories.Implementations.Orders;

namespace AwesomeShop.Services.Orders.Api.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructureExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
        return serviceCollection;
    }
}