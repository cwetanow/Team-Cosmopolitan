using System.ComponentModel.DataAnnotations;
using Factory.InsertData.Models.Products;

namespace Factory.InsertData.Models.Reports
{
    public class ProductSale
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public virtual Report Report { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public decimal Sum { get; set; }
    }
}
