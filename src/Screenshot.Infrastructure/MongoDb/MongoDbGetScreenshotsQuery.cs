using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MongoDB.Driver;

using Screenshot.Domain;

namespace Screenshot.Infrastructure.MongoDb
{
    public class MongoDbGetScreenshotsQuery : IGetScreenshotsQuery
    {
        private readonly IMongoContext _mongoContext;

        public MongoDbGetScreenshotsQuery(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<IEnumerable<Domain.Screenshot>> Execute(CancellationToken cancellationToken)
        {
            return await _mongoContext.Screenshots.Find(_ => true).ToListAsync(cancellationToken);
        }
    }
}