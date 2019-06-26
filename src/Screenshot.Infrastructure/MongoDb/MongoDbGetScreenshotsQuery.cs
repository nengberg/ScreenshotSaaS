using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;

namespace Screenshot.Infrastructure
{
    public class MongoDbGetScreenshotsQuery : IGetScreenshotsQuery
    {
        private readonly MongoContext _mongoContext;

        public MongoDbGetScreenshotsQuery(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<IEnumerable<Screenshot>> Execute()
        {
            return await _mongoContext.Screenshots.Find(_ => true).ToListAsync();
        }
    }
}