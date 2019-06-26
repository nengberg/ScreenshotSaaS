using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;

namespace Screenshot.Infrastructure.MongoDb
{
    public class MongoDbGetScreenshotsQuery : IGetScreenshotsQuery
    {
        private readonly IMongoContext _mongoContext;

        public MongoDbGetScreenshotsQuery(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<IEnumerable<Screenshot>> Execute()
        {
            return await _mongoContext.Screenshots.Find(_ => true).ToListAsync();
        }
    }
}