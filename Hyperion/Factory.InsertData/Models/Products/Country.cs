using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Factory.InsertData.Models.Products
{
    public class Country
    {
        private ICollection<City> cities;

        public Country()
        {
            this.cities = new HashSet<City>();
        }

        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<City> Cities
        {
            get { return this.cities; }
            set { this.cities = value; }
        }
    }
}