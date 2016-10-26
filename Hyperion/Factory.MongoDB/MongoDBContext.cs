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
        private readonly string DataName;

        public MongoDBContext(string dataName)
        {
            DataName = dataName;
            client = new MongoClient();
            dataBase = client.GetDatabase(DataName);
        }

        public List<SpaceshipMap> GetData()
        {
            var collection = this.dataBase.GetCollection<SpaceshipMap>(DataName);
            return collection.Find(new BsonDocument()).ToList();
        }
    }
}
