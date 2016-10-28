using System.ComponentModel.DataAnnotations;

namespace Factory.InsertData.Models.Products
{
    public class Part
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public PartType PartType { get; set; }

        public Supplier Supplier { get; set; }
    }
}
