using MongoDB.Driver;

namespace Screenshot.Infrastructure.MongoDb
{
    public interface IMongoContext
    {
        IMongoCollection<Domain.Screenshot> Screenshots { get; }
    }
}