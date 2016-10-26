using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Factory.MongoDB.ModelMaps
{
   public class SpaceshipMap
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public string Category { get; set; }

        public string Color { get; set; }

        public List<PartMap> Parts { get; set; }
    }
}
