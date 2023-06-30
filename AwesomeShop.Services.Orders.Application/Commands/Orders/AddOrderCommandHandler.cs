using AwesomeShop.Services.Orders.Core.Repositories;
using MediatR;

namespace AwesomeShop.Services.Orders.Application.Commands.Orders;

public class AddOrderCommandHandler : IRequestHandler<AddOrderCommandInputModel, Guid>
{
    private readonly IOrderRepository _orderRepository;

    public AddOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> Handle(AddOrderCommandInputModel request,
        CancellationToken cancellationToken)
    {
        var order = request.ToEntity();
        await _orderRepository.AddAsync(order);
        return order.Id;
    }
}