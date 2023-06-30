using AwesomeShop.Services.Orders.Core.Entities;
using AwesomeShop.Services.Orders.Core.Enums;

namespace AwesomeShop.Services.Orders.Application.Queries.Orders;

public class GetOrderByIdQueryViewModel
{
    public GetOrderByIdQueryViewModel(Guid id, decimal totalPrice, DateTime createdAt, OrderStatusEnum status)
    {
        Id = id;
        TotalPrice = totalPrice;
        CreatedAt = createdAt;
        Status = status;
    }

    public Guid Id { get; private set; }
    public decimal TotalPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatusEnum Status { get; private set; }

    public static GetOrderByIdQueryViewModel FromEntity(Order order)
    {
        return new GetOrderByIdQueryViewModel(order.Id, order.TotalPrice, order.CreatedAt, order.Status);
    }
}