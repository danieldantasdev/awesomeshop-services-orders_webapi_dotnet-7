using AwesomeShop.Services.Orders.Core.Repositories;

namespace AwesomeShop.Services.Orders.Infrastructure.Repositories.Order;

public class OrderRepository : IOrderRepository
{
    public async Task<Core.Entities.Order> GetByIdAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Core.Entities.Order order)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Core.Entities.Order order)
    {
        throw new NotImplementedException();
    }
}