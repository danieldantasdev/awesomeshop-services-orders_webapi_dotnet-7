using AwesomeShop.Services.Orders.Application.Integrations.Customers;
using AwesomeShop.Services.Orders.Application.Methods;
using AwesomeShop.Services.Orders.Core.Repositories.Interfaces;
using AwesomeShop.Services.Orders.Core.Services.Interfaces.Consul;
using AwesomeShop.Services.Orders.Core.Services.Interfaces.Integrations.Customers;
using AwesomeShop.Services.Orders.Core.Services.Interfaces.MessageBus.RabbitMq;
using MediatR;
using Newtonsoft.Json;

namespace AwesomeShop.Services.Orders.Application.Commands.Orders;

public class AddOrderCommandHandler : IRequestHandler<AddOrderCommandInputModel, AddOrderCommandViewModel>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IRabbitMqClientService _rabbitMqClientService;
    private readonly IConsulService _consulService;
    private readonly ICustomerIntegrationService _customerIntegrationService;

    public AddOrderCommandHandler(IOrderRepository orderRepository, IRabbitMqClientService rabbitMqClientService,
        IConsulService consulService, ICustomerIntegrationService customerIntegrationService)
    {
        _orderRepository = orderRepository;
        _rabbitMqClientService = rabbitMqClientService;
        _consulService = consulService;
        _customerIntegrationService = customerIntegrationService;
    }

    public async Task<AddOrderCommandViewModel> Handle(AddOrderCommandInputModel request,
        CancellationToken cancellationToken)
    {
        var order = request.ToEntity();

        var customer = await _customerIntegrationService.GetCustomer(order.Id, cancellationToken);

        await _orderRepository.AddAsync(order);

        foreach (var @event in order.Events)
        {
            //OrderCreated = order-created
            var routingKey = @event.GetType().Name.ToDashCase();
            _rabbitMqClientService.Publish(@event, routingKey, "order-service");
        }

        return new AddOrderCommandViewModel(order.Id);
    }
}