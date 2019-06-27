using System;
using System.IO;
using System.Threading;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Screenshot.Domain;
using Screenshot.Infrastructure;
using Screenshot.Infrastructure.MongoDb;
using Screenshot.Infrastructure.RabbitMq;

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

            services.AddTransient<ISaveScreenshotCommand, MongoDbSaveScreenshotCommand>();
            services.AddTransient<IWebDriverFactory, WebDriverFactory>();
            services.AddTransient<IMessageHandler<GenerateScreenshotMessage>, GenerateScreenshotMessageHandler>();
            services.AddRabbitMq(config);
            services.AddMongoDb(config);

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
