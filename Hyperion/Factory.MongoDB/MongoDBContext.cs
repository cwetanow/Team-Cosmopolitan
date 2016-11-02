using System.Collections.Generic;
using Factory.MongoDB.ModelMaps;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Factory.MongoDB
{
    public class MongoDBContext
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase dataBase;

        public MongoDBContext(string dataName)
        {
            client = new MongoClient("mongodb://localhost");
            dataBase = client.GetDatabase(dataName);
        }

        public List<SpaceshipMap> GetData(string collectionName)
        {
            var collection = this.dataBase.GetCollection<SpaceshipMap>(collectionName);
            return collection.Find(new BsonDocument()).ToList();
        }
    }
}
