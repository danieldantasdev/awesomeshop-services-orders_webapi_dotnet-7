using AwesomeShop.Services.Orders.Core.Repositories;
using AwesomeShop.Services.Orders.Core.Repositories.Interfaces;
using AwesomeShop.Services.Orders.Core.Services.Interfaces.CacheStorages;
using MediatR;

namespace AwesomeShop.Services.Orders.Application.Queries.Orders;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryInputModel, GetOrderByIdQueryViewModel>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICacheStorageService _cacheStorageService;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, ICacheStorageService cacheStorageService)
    {
        _orderRepository = orderRepository;
        _cacheStorageService = cacheStorageService;
    }

    public async Task<GetOrderByIdQueryViewModel> Handle(GetOrderByIdQueryInputModel request,
        CancellationToken cancellationToken)
    {
        var cacheKey = request.Id.ToString();
        var cacheObject = await _cacheStorageService.GetAsync<GetOrderByIdQueryViewModel>(cacheKey);

        if (cacheObject == null)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);
            cacheObject = GetOrderByIdQueryViewModel.FromEntity(order);
            await _cacheStorageService.SetAsync(cacheKey, cacheObject);
        }

        return cacheObject;
    }
}