using AwesomeShop.Services.Orders.Core.Entities;

namespace AwesomeShop.Services.Orders.Core.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order> GetByIdAsync(Guid guid);
    Task UpdateAsync(Order order);
}