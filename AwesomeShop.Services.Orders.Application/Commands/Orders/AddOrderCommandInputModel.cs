using AwesomeShop.Services.Orders.Core.Entities;
using AwesomeShop.Services.Orders.Core.ValueObjects;
using MediatR;

namespace AwesomeShop.Services.Orders.Application.Commands.Orders;

public class AddOrderCommandInputModel : IRequest<AddOrderCommandViewModel>
{
    public CustomerInputModel Customer { get; set; }
    public List<OrderItemInputModel> OrderItems { get; set; }
    public DeliveryAddressInputModel DeliveryAddress { get; set; }
    public PaymentAddressInputModel PaymentAddress { get; set; }
    public PaymentInfoInputModel PaymentInfo { get; set; }

    public Order ToEntity()
    {
        return new Order(
            new Customer
            (
                Customer.Id,
                Customer.FullName,
                Customer.Email
            ),
            new DeliveryAddress
            (
                DeliveryAddress.Street,
                DeliveryAddress.Number,
                DeliveryAddress.City,
                DeliveryAddress.State,
                DeliveryAddress.ZipCode
            ),
            new PaymentAddress
            (
                PaymentAddress.Street,
                PaymentAddress.Number,
                PaymentAddress.City,
                PaymentAddress.State,
                PaymentAddress.ZipCode
            ),
            new PaymentInfo(
                PaymentInfo.CardNumber,
                PaymentInfo.FullName,
                PaymentInfo.ExpirationDate,
                PaymentInfo.Cvv
            ),
            OrderItems.Select(oi => new OrderItem(oi.ProductId, oi.Quantity, oi.Price)).ToList()
        );
    }
}

public class CustomerInputModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}

public class OrderItemInputModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class DeliveryAddressInputModel
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}

public class PaymentAddressInputModel
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}

public class PaymentInfoInputModel
{
    public string CardNumber { get; set; }
    public string FullName { get; set; }
    public string ExpirationDate { get; set; }
    public string Cvv { get; set; }
}