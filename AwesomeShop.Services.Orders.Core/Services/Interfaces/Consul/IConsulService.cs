namespace AwesomeShop.Services.Orders.Core.Services.Interfaces.Consul;

public interface IConsulService
{
    Task<Uri> GetServiceUri(string serviceName, string resquestUrl);
}