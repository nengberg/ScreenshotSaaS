using System;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Screenshot.Infrastructure.RabbitMq
{
    public class RabbitMqMessageBroker : IMessageBroker
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConnection _connection;
        private readonly IModel _subscriptionChannel;

        public RabbitMqMessageBroker(IConnectionFactory connectionFactory, IServiceScopeFactory serviceScopeFactory)
        {
            _connection = connectionFactory.CreateConnection();
            _subscriptionChannel = _connection.CreateModel();
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            using(var channel = _connection.CreateModel())
            {
                Console.WriteLine($"Publishing message {typeof(TMessage).Name}");
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                channel.BasicPublish(
                    exchange: "", //This should be a specific exchange
                    routingKey: typeof(TMessage).Name,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            }
        }

        public void Subscribe<TMessage>() where TMessage : IMessage
        {
            var queueName = typeof(TMessage).Name;
            Console.WriteLine($"Subscribing to queue {queueName}.");

            _subscriptionChannel.QueueDeclare(queueName, true, false, false, null);

            var consumer = new EventingBasicConsumer(_subscriptionChannel);
            consumer.Received += DispatchToMessageHandler<TMessage>();
            _subscriptionChannel.BasicConsume(queueName, true, consumer);
        }

        private EventHandler<BasicDeliverEventArgs> DispatchToMessageHandler<TMessage>() where TMessage : IMessage
        {
            return (s, eventArgs) =>
            {
                var body = eventArgs.Body;
                var message = Encoding.UTF8.GetString(body);
                var concreteMessage = JsonConvert.DeserializeObject<TMessage>(message);
                Console.WriteLine($"Processing message {typeof(TMessage).Name}");
                var concreteType = typeof(IMessageHandler<>).MakeGenericType(typeof(TMessage));
                using(var scope = _serviceScopeFactory.CreateScope())
                {                  
                    var handler = (dynamic)scope.ServiceProvider.GetService(concreteType);
                    handler.Handle((dynamic)concreteMessage);
                }
            };
        }
    }
}