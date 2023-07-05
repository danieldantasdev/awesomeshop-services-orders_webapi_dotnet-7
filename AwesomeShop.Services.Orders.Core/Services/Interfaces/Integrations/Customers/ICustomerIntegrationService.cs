namespace AwesomeShop.Services.Orders.Core.Services.Interfaces.Integrations.Customers;

public interface ICustomerIntegrationService
{
    Task<object> GetCustomer(Guid id, CancellationToken cancellationToken);
}