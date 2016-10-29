using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.InsertData.Models.Reports
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<ProductSale> Sales { get; set; }

        public decimal TotalSum { get; set; }

        public DateTime Date { get; set; }
    }
}
