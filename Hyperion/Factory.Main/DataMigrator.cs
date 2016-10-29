using System.Collections.Generic;
using System.Collections.ObjectModel;
using Factory.ExcelReports.Models;
using Factory.InsertData.Models.Reports;

namespace Factory.Main
{
    public class DataMigrator
    {
        private static DataMigrator instance;

        private static object lockObject = new object();

        private DataMigrator() { }

        public static DataMigrator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new DataMigrator();
                        }
                    }
                }

                return instance;
            }
        }

        private ProductSale GetProductSale(Sale sale)
        {
            var newSale = new ProductSale
            {
                Price = sale.UnitPrice,
                Quantity = sale.Quantity,
                ProductName = sale.ProductName
            };

            newSale.Sum = newSale.Price * newSale.Quantity;

            return newSale;
        }

        public ICollection<Report> GetReports(ICollection<ExcelReport> reports)
        {
            var newReports = new Collection<Report>();

            foreach (var excelReport in reports)
            {
                var report = this.GetReport(excelReport);

                newReports.Add(report);
            }

            return newReports;
        }

        private Report GetReport(ExcelReport excelReport)
        {
            var report = new Report
            {
                Date = excelReport.Date,
                TotalSum = excelReport.GetTotalSum()
            };

            var excelSales = excelReport.GetSales();
            var sales = new Collection<ProductSale>();

            foreach (var productSale in excelSales)
            {
                var sale = this.GetProductSale(productSale);

                sales.Add(sale);
            }

            report.Sales = sales;
            return report;
        }
    }
}
