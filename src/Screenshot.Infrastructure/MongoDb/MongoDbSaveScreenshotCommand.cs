using System.Threading.Tasks;

namespace Screenshot.Infrastructure.MongoDb
{
    public class MongoDbSaveScreenshotCommand : ISaveScreenshotCommand
    {
        private readonly IMongoContext _mongoContext;

        public MongoDbSaveScreenshotCommand(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task Execute(Screenshot screenshot)
        {
            await _mongoContext.Screenshots.InsertOneAsync(screenshot);
        }
    }
}