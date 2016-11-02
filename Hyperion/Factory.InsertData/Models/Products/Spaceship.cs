using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Factory.InsertData.Models.Products
{
    public class Spaceship
    {
        private ICollection<Part> parts;

        public Spaceship()
        {
            this.parts = new HashSet<Part>();
        }

        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("model")]
        [StringLength(50)]
        public string Model { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("categoryId")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [JsonProperty("category")]
        public virtual Category Category { get; set; }

        [JsonProperty("color")]
        [StringLength(50)]
        public string Color { get; set; }

        [JsonProperty("parts")]
        public virtual ICollection<Part> Parts
        {
            get { return this.parts; }
            set { this.parts = value; }
        }
    }
}
