using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDB.Bson.Serialization;

namespace Screenshot.Infrastructure.MongoDb
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            BsonClassMap.RegisterClassMap<Screenshot>(cm =>
            {
                cm.MapIdField(s => s.Id);
                cm.AutoMap();
            });
            services.AddSingleton<MongoContext>(_ =>
            {
                var mongoSettings = new MongoDbSettings();
                configuration.GetSection("MongoDbSettings").Bind(mongoSettings);
                return new MongoContext(mongoSettings);
            });
            return services;
        }
    }
}