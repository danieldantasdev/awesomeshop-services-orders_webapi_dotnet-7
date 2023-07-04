namespace AwesomeShop.Services.Orders.Core.MessageBus.Interfaces.Subscribers;

public class PaymentAccepted
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
}