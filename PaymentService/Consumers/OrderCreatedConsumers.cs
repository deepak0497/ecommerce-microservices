
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class OrderCreatedConsumer : BackgroundService

{


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {

            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        channel.ExchangeDeclare("order_exchange", ExchangeType.Fanout);
        var queue = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue, "order_exchange", "");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, e) =>
        {
            var json = Encoding.UTF8.GetString(e.Body.ToArray());

            var orderEvent = JsonConvert.DeserializeObject<OrderCreatedEvent>(json);
            Console.WriteLine($"Payment Processing for Order: {orderEvent.OrderId}");

        };

        channel.BasicConsume(queue, true, consumer);

        return Task.CompletedTask;
    }
}