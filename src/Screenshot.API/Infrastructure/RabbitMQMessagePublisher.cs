using System;
using System.Text;

using RabbitMQ.Client;

namespace Screenshot.API.Infrastructure
{
    public class RabbitMQMessagePublisher : IMessagePublisher
    {
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMQMessagePublisher(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Publish(string message)
        {
            using(var conn = _connectionFactory.CreateConnection())
            using(var channel = conn.CreateModel())
            {
                Console.WriteLine("Publishing message");
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "urls",
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            }
        }
    }
}