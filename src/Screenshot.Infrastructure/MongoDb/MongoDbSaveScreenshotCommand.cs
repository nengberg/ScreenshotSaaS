using System.Threading.Tasks;

using Screenshot.Domain;

namespace Screenshot.Infrastructure.MongoDb
{
    public class MongoDbSaveScreenshotCommand : ISaveScreenshotCommand
    {
        private readonly IMongoContext _mongoContext;

        public MongoDbSaveScreenshotCommand(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task Execute(Domain.Screenshot screenshot)
        {
            await _mongoContext.Screenshots.InsertOneAsync(screenshot);
        }
    }
}