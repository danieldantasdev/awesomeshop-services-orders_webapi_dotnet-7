using AwesomeShop.Services.Orders.Application.Commands.Orders;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.Services.Orders.Application.Extensions;

public static class MediatRExtension
{
    public static IServiceCollection AddMediatRExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(typeof(AddOrderCommandInputModel));
        return serviceCollection;
    }
}