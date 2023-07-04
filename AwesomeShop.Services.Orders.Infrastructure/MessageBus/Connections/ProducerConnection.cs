using RabbitMQ.Client;

namespace AwesomeShop.Services.Orders.Infrastructure.MessageBus.Connections;

public class ProducerConnection
{
    public ProducerConnection(IConnection connection)
    {
        Connection = connection;
    }

    public IConnection Connection { get; private set; }
}