using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;

namespace Screenshot.Infrastructure.RabbitMq
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMessageBroker>(
                c =>
                {
                    var connectionFactory = new ConnectionFactory();
                    configuration.GetSection("RabbitMqConnection").Bind(connectionFactory);
                    return new RabbitMqMessageBroker(connectionFactory, c.GetService<IServiceScopeFactory>());
                });

            return services;
        }
    }
}