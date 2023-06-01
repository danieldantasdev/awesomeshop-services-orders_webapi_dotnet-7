using AwesomeShop.Services.Orders.Core.Entities;

namespace AwesomeShop.Services.Orders.Core.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid guid);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}