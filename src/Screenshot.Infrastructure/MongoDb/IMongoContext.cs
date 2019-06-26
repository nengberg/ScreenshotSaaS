using MongoDB.Driver;

namespace Screenshot.Infrastructure.MongoDb
{
    public interface IMongoContext
    {
        IMongoCollection<Screenshot> Screenshots { get; }
    }
}