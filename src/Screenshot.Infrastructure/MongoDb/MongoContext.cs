using MongoDB.Driver;

namespace Screenshot.Infrastructure
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(MongoDbSettings dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.ConnectionString);
            _database = mongoClient.GetDatabase(dbSettings.DatabaseName);
        }

        public IMongoCollection<Screenshot> Screenshots => _database.GetCollection<Screenshot>(nameof(Screenshot));
    }
}