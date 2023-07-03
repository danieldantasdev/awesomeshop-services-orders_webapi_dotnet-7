using System.Text;
using AwesomeShop.Services.Orders.Core.MessageBus.Interfaces.RabbitMq;
using AwesomeShop.Services.Orders.Infrastructure.MessageBus.Connections;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace AwesomeShop.Services.Orders.Infrastructure.MessageBus.Implementations.RabbitMq;

public class RabbitMqClient : IRabbitMqClient
{
    private readonly IConnection _connection;

    public RabbitMqClient(ProducerConnection producerConnection)
    {
        _connection = producerConnection.Connection;
    }

    public void Publish(object message, string routingKey, string exchange)
    {
        var channel = _connection.CreateModel();

        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        var payload = JsonConvert.SerializeObject(message, settings);
        var body = Encoding.UTF8.GetBytes(payload);

        channel.ExchangeDeclare(exchange, "topic", true);
        channel.BasicPublish(exchange, routingKey, null, body);
    }
}