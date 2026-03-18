// Services/OrderService.cs
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace OrderService.Services
{
    public class OrderServiceHandler
    {
        private readonly string _rabbitMqConnection = "amqp://localhost"; // RabbitMQ default URL
        private readonly string _exchangeName = "order_exchange";
        private readonly string _queueName = "order_queue";

        public void PublishOrderCreatedEvent(Order order)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_rabbitMqConnection) };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(order));

                // Publish to the exchange
                channel.BasicPublish(exchange: _exchangeName,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}