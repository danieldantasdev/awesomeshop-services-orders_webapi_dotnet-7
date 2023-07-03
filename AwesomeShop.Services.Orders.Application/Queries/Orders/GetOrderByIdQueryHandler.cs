using AwesomeShop.Services.Orders.Core.Repositories;
using AwesomeShop.Services.Orders.Core.Repositories.Interfaces;
using MediatR;

namespace AwesomeShop.Services.Orders.Application.Queries.Orders;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryInputModel, GetOrderByIdQueryViewModel>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetOrderByIdQueryViewModel> Handle(GetOrderByIdQueryInputModel request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        var orderViewModel = GetOrderByIdQueryViewModel.FromEntity(order);
        return orderViewModel;
    }
}