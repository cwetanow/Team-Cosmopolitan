using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.InsertData.Models.Reports
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public decimal TotalSum { get; set; }
    }
}
