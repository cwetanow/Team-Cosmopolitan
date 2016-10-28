using System.ComponentModel.DataAnnotations;

namespace Factory.InsertData.Models.Products
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public Country Country { get; set; }
    }
}
