using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.InsertData.Models.Products
{
    public class Spaceship
    {
        private ICollection<Part> parts;

        public Spaceship()
        {
            this.parts = new HashSet<Part>();
        }
        
        [Key]
        public int Id { get; set; }
        
        [StringLength(50)]
        public string Model { get; set; }
        
        public int Year { get; set; }
        
        public decimal Price { get; set; }
        
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        
        public virtual Category Category { get; set; }
        
        [StringLength(50)]
        public string Color { get; set; }
        
        public virtual ICollection<Part> Parts
        {
            get { return this.parts; }
            set { this.parts = value; }
        }
    }
}
