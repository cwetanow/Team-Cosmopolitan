using System;
using System.Collections.Generic;

namespace Factory.ExcelReports.Models
{
    public class ExcelReport
    {
        private readonly IList<Sale> sales;

        public ExcelReport()
        {
            this.sales = new List<Sale>();
        }
        
        public DateTime Date { get; set; }

        public void AddSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentException("Sale cannot be null!");
            }

            this.sales.Add(sale);
        }

        public IList<Sale> GetSales()
        {
            return this.sales;
        }

        public decimal GetTotalSum()
        {
            decimal totalSum = 0;
            foreach (var sale in this.sales)
            {
                totalSum += sale.Sum;
            }

            return totalSum;
        }
    }
}
