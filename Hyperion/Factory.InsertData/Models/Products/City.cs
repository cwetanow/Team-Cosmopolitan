using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.InsertData.Models.Products
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }
        
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        
        public virtual Country Country { get; set; }
    }
}