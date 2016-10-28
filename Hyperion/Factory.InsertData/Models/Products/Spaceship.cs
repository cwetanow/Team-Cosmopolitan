using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.InsertData.Models.Products
{
    public class Spaceship
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
    }
}
