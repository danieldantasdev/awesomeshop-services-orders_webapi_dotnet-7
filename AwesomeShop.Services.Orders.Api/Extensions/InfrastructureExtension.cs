using AwesomeShop.Services.Orders.Application.Subscribers;
using AwesomeShop.Services.Orders.Core.Repositories.Interfaces;
using AwesomeShop.Services.Orders.Core.Services.Interfaces.Consul;
using AwesomeShop.Services.Orders.Infrastructure.Persistence.Repositories.Implementations.Orders;
using AwesomeShop.Services.Orders.Infrastructure.Services.Implementations;
using AwesomeShop.Services.Orders.Infrastructure.Services.Implementations.Consuls;
using Consul;

namespace AwesomeShop.Services.Orders.Api.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructureExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
        serviceCollection.AddHostedService<PaymentAcceptedSubscriber>();
        return serviceCollection;
    }

    public static IServiceCollection AddConsulExtension(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IConsulClient, ConsulClient>(c => new ConsulClient(consulConfiguration =>
        {
            var address = configuration.GetValue<string>("Consul:Host");
            if (address != null) consulConfiguration.Address = new Uri(address);
        }));

        serviceCollection.AddTransient<IConsulService, ConsulService>();
        return serviceCollection;
    }

    public static IApplicationBuilder UseConsul(this IApplicationBuilder applicationBuilder)
    {
        var consulClient = applicationBuilder.ApplicationServices.GetRequiredService<IConsulClient>();
        var lifetime = applicationBuilder.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        var registration = new AgentServiceRegistration
        {
            ID = $"order-service-{Guid.NewGuid()}",
            Name = "OrderServices",
            Address = "localhost",
            Port = 5003
        };

        consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
        consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);
        
        Console.WriteLine("Service Registered in Consul");

        lifetime.ApplicationStopping.Register(() =>
        {
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            Console.WriteLine("Service Deregistered in Consul");
        });

        return applicationBuilder;
    }
}