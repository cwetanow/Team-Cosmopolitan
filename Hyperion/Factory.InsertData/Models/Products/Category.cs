using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.InsertData.Models.Products
{
    public class Category
    {
        private ICollection<Spaceship> spaceships;

        public Category()
        {
            this.spaceships = new HashSet<Spaceship>();
        }

        [Key]
        public int Id { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }
        
        public virtual ICollection<Spaceship> Spaceships
        {
            get { return this.spaceships; }
            set { this.spaceships = value; }
        }
    }
}