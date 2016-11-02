using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Factory.InsertData.Models.Products
{
    public class Category
    {
        private ICollection<Spaceship> spaceships;

        public Category()
        {
            this.spaceships = new HashSet<Spaceship>();
        }

        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Spaceship> Spaceships
        {
            get { return this.spaceships; }
            set { this.spaceships = value; }
        }
    }
}