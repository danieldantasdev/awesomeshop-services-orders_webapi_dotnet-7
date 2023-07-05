using AwesomeShop.Services.Orders.Core.Services.Interfaces.Consul;
using AwesomeShop.Services.Orders.Core.Services.Interfaces.Integrations.Customers;
using Newtonsoft.Json;

namespace AwesomeShop.Services.Orders.Infrastructure.Services.Implementations.Integrations.Customer;

public class CustomerIntegrationService : ICustomerIntegrationService
{
    private readonly IConsulService _consulService;

    public CustomerIntegrationService(IConsulService consulService)
    {
        _consulService = consulService;
    }

    public async Task<object> GetCustomer(Guid id, CancellationToken cancellationToken)
    {
        var customerUri = await _consulService.GetServiceUri("CustomerServices", $"api/customers/{id}");
        var httpClient = new HttpClient();
        var result = await httpClient.GetAsync(customerUri, cancellationToken);
        var stringResult = await result.Content.ReadAsStringAsync(cancellationToken);
        Console.WriteLine(stringResult);

        var customer = JsonConvert.DeserializeObject<object>(stringResult);
        return customer!;
    }
}