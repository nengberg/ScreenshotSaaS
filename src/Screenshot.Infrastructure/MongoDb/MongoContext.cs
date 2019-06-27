using MongoDB.Driver;

namespace Screenshot.Infrastructure.MongoDb
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(MongoDbSettings dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.ConnectionString);
            _database = mongoClient.GetDatabase(dbSettings.DatabaseName);
        }

        public IMongoCollection<Domain.Screenshot> Screenshots => _database.GetCollection<Domain.Screenshot>(nameof(Screenshot));
    }
}