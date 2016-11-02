using Factory.LoadXML.Models;
using Factory.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Factory.LoadXML
{
    public class FactoryXmlImporter
    {
        private XmlSerializer serializer;

        public FactoryXmlImporter()
        {
        }

        public void ImportDataFromXml(MongoDBContext database, string path)
        {
            this.serializer = new XmlSerializer(typeof(List<Spaceship>));

            var spaceships = new List<Spaceship>();

            using (var reader = new StreamReader(path))
            {
                spaceships = this.serializer.Deserialize(reader) as List<Spaceship>;

            }

            var collection = database.GetCollection<Spaceship>("Spaceships");

            collection.InsertMany(spaceships);
        }
    }
}
