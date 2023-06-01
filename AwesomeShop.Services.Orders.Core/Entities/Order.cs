using AwesomeShop.Services.Orders.Core.Enums;
using AwesomeShop.Services.Orders.Core.Events;
using AwesomeShop.Services.Orders.Core.ValueObjects;

namespace AwesomeShop.Services.Orders.Core.Entities;

public class Order : AggregateRoot
{
    public Order(Customer customer, DeliveryAddress deliveryAddress, PaymentAddress paymentAddress,
        PaymentInfo paymentInfo, List<OrderItem> orderItems)
    {
        Id = Guid.NewGuid();
        TotalPrice = orderItems.Sum(i => i.Quantity * i.Price);
        Customer = customer;
        DeliveryAddress = deliveryAddress;
        PaymentAddress = paymentAddress;
        PaymentInfo = paymentInfo;
        OrderItems = orderItems;
        CreatedAt = DateTime.Now;
        AddEvent(new OrderCreated(Id, TotalPrice, PaymentInfo, Customer.FullName, Customer.Email));
    }

    public decimal TotalPrice { get; private set; }
    public Customer Customer { get; private set; }
    public DeliveryAddress DeliveryAddress { get; private set; }
    public PaymentAddress PaymentAddress { get; private set; }
    public PaymentInfo PaymentInfo { get; private set; }
    public List<OrderItem> OrderItems { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

    public void SetAsCompleted()
    {
        OrderStatus = OrderStatus.Completed;
    }
    
    public void SetAsRejected()
    {
        OrderStatus = OrderStatus.Rejected;
    }
}