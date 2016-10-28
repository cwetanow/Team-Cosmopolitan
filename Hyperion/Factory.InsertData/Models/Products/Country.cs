using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.InsertData.Models.Products
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
