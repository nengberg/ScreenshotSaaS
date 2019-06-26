using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;

using Screenshot.Infrastructure;

namespace Screenshot.Processor
{
    public class Program
    {
        private static readonly AutoResetEvent WaitHandle = new AutoResetEvent(false);

        public static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();

            services.AddTransient<IMessageHandler<GenerateScreenshotMessage>, GenerateScreenshotMessageHandler>();
            services.AddSingleton<IMessageBroker>(
                c =>
                {
                    var connectionFactory = new ConnectionFactory();
                    config.GetSection("RabbitMqConnection").Bind(connectionFactory);
                    return new RabbitMqMessageBroker(connectionFactory, c.GetService<IServiceScopeFactory>());
                });
            var serviceProvider = services.BuildServiceProvider();

            var messageBroker = serviceProvider.GetService<IMessageBroker>();

            messageBroker.Subscribe<GenerateScreenshotMessage>();


            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Exit");
                WaitHandle.Set();
            };

            WaitHandle.WaitOne();
        }
    }
}
