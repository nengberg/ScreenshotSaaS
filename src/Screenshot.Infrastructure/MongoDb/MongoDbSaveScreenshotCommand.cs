using System.Threading.Tasks;

namespace Screenshot.Infrastructure.MongoDb
{
    public class MongoDbSaveScreenshotCommand : ISaveScreenshotCommand
    {
        private readonly MongoContext _mongoContext;

        public MongoDbSaveScreenshotCommand(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task Execute(Screenshot screenshot)
        {
            await _mongoContext.Screenshots.InsertOneAsync(screenshot);
        }
    }
}