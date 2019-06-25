using System;
using System.IO;
using System.Text;
using System.Threading;

using Microsoft.Extensions.Configuration;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Screenshot.Processor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            ConfigureRabbitMq(config);
        }

        private static void ConfigureRabbitMq(IConfiguration config)
        {
            var connectionFactory = new ConnectionFactory();
            config.GetSection("RabbitMqConnection").Bind(connectionFactory);

            using(var conn = connectionFactory.CreateConnection())
                using(var channel = conn.CreateModel())
                {
                    Console.WriteLine("Running!");

                    const string queueName = "urls";
                    channel.QueueDeclare(queueName, true, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += ProcessUrlAndCreateScreenshot();
                    channel.BasicConsume(queueName, true, consumer);

                    Console.ReadLine();
                }
        }

        private static EventHandler<BasicDeliverEventArgs> ProcessUrlAndCreateScreenshot()
        {
            return (s, eventArgs) =>
            {
                var body = eventArgs.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Message received! {message}");
                var command = new CaptureScreenshotMessage(message);
                var commandHandler = new CaptureScreenshotMessageHandler();
                commandHandler.Handle(command);
            };
        }
    }
}
