using System.Text;
using AwesomeShop.Services.Orders.Core.Repositories.Interfaces;
using AwesomeShop.Services.Orders.Core.Services.Interfaces.MessageBus.Subscribers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AwesomeShop.Services.Orders.Application.Subscribers;

public class PaymentAcceptedSubscriber : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _model;
    private const string Queue = "order-service/payment-accepted";
    private const string Exchange = "order-service";
    private const string RoutingKey = "payment-accepted";

    public PaymentAcceptedSubscriber(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        _connection = connectionFactory.CreateConnection("order-service-payment-accepted-subscriber");

        _model = _connection.CreateModel();
        _model.ExchangeDeclare(Exchange, "topic", true);
        _model.QueueDeclare(Queue, false, false, false, null);
        _model.QueueBind(Queue, "payment-service", RoutingKey);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_model);
        consumer.Received += async (sender, eventArgs) =>
        {
            var byteArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(byteArray);
            var message = JsonConvert.DeserializeObject<PaymentAccepted>(contentString);
            Console.WriteLine($"Message PaymentAccepted received with Id {message.Id}");

            await UpdateOrder(message);
            _model.BasicAck(eventArgs.DeliveryTag, false);
        };

        _model.BasicConsume(Queue, false, consumer);
        return Task.CompletedTask;
    }

    private async Task<bool> UpdateOrder(PaymentAccepted paymentAccepted)
    {
        using var scope = _serviceProvider.CreateScope();
        var orderRepository = scope.ServiceProvider.GetService<IOrderRepository>();
        var order = await orderRepository?.GetByIdAsync(paymentAccepted.Id)!;
        order.SetAsCompleted();
        await orderRepository.UpdateAsync(order);
        return true;
    }
}