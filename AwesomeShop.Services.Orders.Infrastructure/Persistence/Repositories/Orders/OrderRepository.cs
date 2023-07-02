using AwesomeShop.Services.Orders.Core.Repositories;
using MongoDB.Driver;
using AwesomeShop.Services.Orders.Core.Entities;

namespace AwesomeShop.Services.Orders.Infrastructure.Persistence.Repositories.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _mongoCollection;

    public OrderRepository(IMongoDatabase mongoDatabase)
    {
        _mongoCollection = mongoDatabase.GetCollection<Order>("orders");
    }

    public async Task AddAsync(Order order)
    {
        await _mongoCollection.InsertOneAsync(order);
    }

    public async Task<Order> GetByIdAsync(Guid guid)
    {
        return await _mongoCollection.Find(o => o.Id == guid).SingleOrDefaultAsync();
    }


    public async Task UpdateAsync(Order order)
    {
        await _mongoCollection.ReplaceOneAsync(o => o.Id == order.Id, order);
    }
}