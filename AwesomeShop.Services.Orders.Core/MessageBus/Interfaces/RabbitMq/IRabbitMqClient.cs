namespace AwesomeShop.Services.Orders.Core.MessageBus.Interfaces.RabbitMq;

public interface IRabbitMqClient
{
    void Publish(object message, string routingKey, string exchange);
}