using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Factory.InsertData.Models.Products
{
    public class Part
    {
        public Part()
        {
            this.Spaceships = new HashSet<Spaceship>();
        }

        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("partTypeId")]
        [ForeignKey("PartType")]
        public int PartTypeId { get; set; }

        [JsonProperty("partType")]
        public virtual PartType PartType { get; set; }

        [JsonProperty("supplierId")]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        [JsonProperty("supplier")]
        public virtual Supplier Supplier { get; set; }

        [JsonIgnore]
        public virtual ICollection<Spaceship> Spaceships { get; set; }
    }
}
