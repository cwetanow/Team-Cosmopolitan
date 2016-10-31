using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Factory.InsertData.Models.Products
{
    public class Part
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("PartType")]
        public int PartTypeId { get; set; }

        public virtual PartType PartType { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
