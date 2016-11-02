using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Factory.InsertData.Models.Products
{
    public class City
    {
        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [JsonProperty("countryId")]
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [JsonProperty("country")]
        public virtual Country Country { get; set; }
    }
}