using System.Collections.Generic;
using System.IO;
using Factory.InsertData.Models.Products;
using Newtonsoft.Json;

namespace Factory.JsonReports
{
    public class JsonReportsWriter
    {
        private readonly ICollection<Spaceship> Spaceships;

        public JsonReportsWriter(ICollection<Spaceship> spaceships)
        {
            this.Spaceships = spaceships;
        }

        public void WriteReportsToJson(string resultFilesPath)
        {
            foreach (var ship in this.Spaceships)
            {
                var json = JsonConvert.SerializeObject(ship, Formatting.Indented);
                File.WriteAllText($"{resultFilesPath}{ship.Id}.json", json);
            }
        }
    }
}