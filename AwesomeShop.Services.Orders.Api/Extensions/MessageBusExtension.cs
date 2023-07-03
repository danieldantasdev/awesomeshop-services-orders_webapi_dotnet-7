using System.Text;
using AwesomeShop.Services.Orders.Core.MessageBus.Interfaces.RabbitMq;
using AwesomeShop.Services.Orders.Infrastructure.MessageBus.Connections;
using AwesomeShop.Services.Orders.Infrastructure.MessageBus.Implementations.RabbitMq;
using RabbitMQ.Client;

namespace AwesomeShop.Services.Orders.Api.Extensions;

public static class MessageBusExtension
{
    public static IServiceCollection AddRabbitMqExtension(this IServiceCollection serviceCollection)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        var connection = connectionFactory.CreateConnection("order-service-producer");
        serviceCollection.AddSingleton(new ProducerConnection(connection));
        serviceCollection.AddSingleton<IRabbitMqClient, RabbitMqClient>();

        return serviceCollection;
    }

    public static string ToDashCase(this string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (text.Length < 2)
        {
            return text;
        }

        var stringBuilder = new StringBuilder();
        stringBuilder.Append(char.ToLowerInvariant(text[0]));
        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                stringBuilder.Append('-');
                stringBuilder.Append(char.ToLowerInvariant(c));
            }
            else
            {
                stringBuilder.Append(c);
            }
        }
        Console.WriteLine($"ToDashCase: {stringBuilder.ToString()}");
        return stringBuilder.ToString();

    }
}