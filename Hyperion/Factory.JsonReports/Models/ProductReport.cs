using Newtonsoft.Json;

namespace Factory.JsonReports.Models
{
    public class ProductReport
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity-sold")]
        public int QuanitySold { get; set; }

        [JsonProperty("total-income")]
        public decimal TotalIncome { get; set; }
    }
}
