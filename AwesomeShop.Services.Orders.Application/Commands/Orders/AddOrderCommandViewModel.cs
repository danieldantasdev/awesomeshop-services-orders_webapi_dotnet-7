namespace AwesomeShop.Services.Orders.Application.Commands.Orders;

public class AddOrderCommandViewModel
{
    public AddOrderCommandViewModel(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}