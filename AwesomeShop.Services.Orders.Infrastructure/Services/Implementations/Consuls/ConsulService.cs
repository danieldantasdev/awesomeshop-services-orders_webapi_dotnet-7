using AwesomeShop.Services.Orders.Core.Services.Interfaces.Consul;
using Consul;

namespace AwesomeShop.Services.Orders.Infrastructure.Services.Implementations.Consuls;

public class ConsulService : IConsulService
{
    private readonly IConsulClient _consulClient;

    public ConsulService(IConsulClient consulClient)
    {
        _consulClient = consulClient;
    }

    public async Task<Uri> GetServiceUri(string serviceName, string resquestUrl)
    {
        var allRegisteredServices = await _consulClient.Agent.Services();
        var registeredService = allRegisteredServices.Response?
            .Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
            .Select(s => s.Value)
            .ToList();


        var service = registeredService!.First();
        Console.WriteLine(service.Address);

        //http://localhost:5002/api/customers/34567890-0987654
        var uri = $"http://{service.Address}:{service.Port}/{resquestUrl}";
        return new Uri(uri);
    }
}