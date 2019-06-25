//using MongoDB.Bson.Serialization;
//using MongoDB.Driver;

//namespace Screenshot.API.Infrastructure
//{
//    public class MongoContext
//    {
//        private readonly MongoClient _mongoClient;
//        private IMongoDatabase _database;

//        public MongoContext(string connectionString, string databaseName)
//        {
//            _mongoClient = new MongoClient(connectionString);
//            _database = _mongoClient.GetDatabase(databaseName);
//            Map();
//        }

//        public IMongoCollection<Controllers.Todo> Todos => _database.GetCollection<Controllers.Todo>("Todos");

//        private static void Map()
//        {
//            BsonClassMap.RegisterClassMap<Controllers.Todo>(cm =>
//            {
//                cm.AutoMap();
//            });
//        }
//    }
//}