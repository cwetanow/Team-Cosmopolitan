using System.ComponentModel.DataAnnotations;
using Factory.InsertData.Models.Products;

namespace Factory.InsertData.Models.Reports
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public Report Report { get; set; }

        [StringLength(50)]
        public string SpaceshipName { get; set; }

        public decimal Price { get; set; }

        public decimal Sum { get; set; }
    }
}
