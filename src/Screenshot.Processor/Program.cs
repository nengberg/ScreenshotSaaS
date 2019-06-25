using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Screenshot.Processor
{
    public class Program
    {
        // AutoResetEvent to signal when to exit the application.
        private static readonly AutoResetEvent waitHandle = new AutoResetEvent(false);

        public static void Main(string[] args)
        {
            // Fire and forget
            Task.Run(() =>
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                    .AddEnvironmentVariables()
                    .Build();

                ConfigureRabbitMq(config);
            });

            // Handle Control+C or Control+Break
            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Exit");

                // Allow the manin thread to continue and exit...
                waitHandle.Set();
            };

            // Wait
            waitHandle.WaitOne();
        }

        private static void ConfigureRabbitMq(IConfiguration config)
        {
            var connectionFactory = new ConnectionFactory();
            config.GetSection("RabbitMqConnection").Bind(connectionFactory);

            using(var conn = connectionFactory.CreateConnection())
            using(var channel = conn.CreateModel())
            {
                const string queueName = "urls";
                Console.WriteLine($"Subscribing to queue {queueName}.");

                channel.QueueDeclare(queueName, true, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += ProcessUrlAndCreateScreenshot();
                channel.BasicConsume(queueName, true, consumer);
                Console.Read();
            }
        }

        private static EventHandler<BasicDeliverEventArgs> ProcessUrlAndCreateScreenshot()
        {
            return (s, eventArgs) =>
            {
                var body = eventArgs.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Processing URL {message} and creating screen shot");
                var captureScreenshotMessage = new CaptureScreenshotMessage(message);
                var messageHandler = new CaptureScreenshotMessageHandler();
                messageHandler.Handle(captureScreenshotMessage);
            };
        }
    }
}
