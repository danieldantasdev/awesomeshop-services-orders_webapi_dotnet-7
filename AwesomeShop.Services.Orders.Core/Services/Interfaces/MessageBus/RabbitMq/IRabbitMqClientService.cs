namespace AwesomeShop.Services.Orders.Core.Services.Interfaces.MessageBus.RabbitMq;

public interface IRabbitMqClientService
{
    void Publish(object message, string routingKey, string exchange);
}