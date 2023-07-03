using AwesomeShop.Services.Orders.Application.Methods;
using AwesomeShop.Services.Orders.Core.MessageBus.Interfaces.RabbitMq;
using AwesomeShop.Services.Orders.Core.Repositories.Interfaces;
using MediatR;

namespace AwesomeShop.Services.Orders.Application.Commands.Orders;

public class AddOrderCommandHandler : IRequestHandler<AddOrderCommandInputModel, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IRabbitMqClient _rabbitMqClient;

    public AddOrderCommandHandler(IOrderRepository orderRepository, IRabbitMqClient rabbitMqClient)
    {
        _orderRepository = orderRepository;
        _rabbitMqClient = rabbitMqClient;
    }

    public async Task<Guid> Handle(AddOrderCommandInputModel request,
        CancellationToken cancellationToken)
    {
        var order = request.ToEntity();
        await _orderRepository.AddAsync(order);

        foreach (var @event in order.Events)
        {
            //OrderCreated = order-created
            var routingKey = @event.GetType().Name.ToDashCase();
            _rabbitMqClient.Publish(@event, routingKey, "order-service");
        }
        
        return order.Id;
    }
}