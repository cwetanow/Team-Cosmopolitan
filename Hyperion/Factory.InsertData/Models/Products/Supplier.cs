using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Factory.InsertData.Models.Products
{
    public class Supplier
    {
        private ICollection<Part> parts;

        public Supplier()
        {
            this.parts = new HashSet<Part>();
        }

        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [JsonProperty("address")]
        [StringLength(50)]
        public string Address { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }

        [JsonIgnore]
        public virtual ICollection<Part> Parts
        {
            get { return this.parts; }
            set { this.parts = value; }
        }
    }
}