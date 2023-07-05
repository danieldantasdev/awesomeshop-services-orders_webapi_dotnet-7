namespace AwesomeShop.Services.Orders.Core.Services.Interfaces.MessageBus.Subscribers;

public class PaymentAccepted
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
}