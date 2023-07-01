using AwesomeShop.Services.Orders.Application.Commands.Orders;
using MediatR;

namespace AwesomeShop.Services.Orders.Api.Extensions;

public static class MediatRExtension
{
    public static IServiceCollection AddMediatRExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly,
                typeof(AddOrderCommandInputModel).Assembly);
        });
        return serviceCollection;
    }
}