using System.Collections.Generic;
using System.IO;
using System.Linq;
using Factory.InsertData.Models.Products;
using Factory.InsertData.Models.Reports;
using Factory.JsonReports.Models;
using Newtonsoft.Json;

namespace Factory.JsonReports
{
    public class JsonReportsHandler
    {
        private readonly ICollection<Spaceship> Spaceships;
        private readonly ICollection<ProductSale> Sales;
        private readonly ICollection<ProductReport> Reports;

        public JsonReportsHandler(ICollection<Spaceship> spaceships, ICollection<ProductSale> sales, ICollection<ProductReport> reports)
        {
            this.Spaceships = spaceships;
            this.Sales = sales;
            this.Reports = reports;
            PopulateReportsList();
        }

        public void WriteReportsToJson(string resultFilesPath)
        {
            foreach (var report in this.Reports)
            {
                var json = JsonConvert.SerializeObject(report, Formatting.Indented);
                File.WriteAllText($"{resultFilesPath}{report.Id}.json", json);
            }
        }

        public ICollection<string> GetReportsInJsonFormat()
        {
            var reports = new List<string>();

            foreach (var report in this.Reports)
            {
                var json = JsonConvert.SerializeObject(report, Formatting.Indented);
                reports.Add(json);
            }

            return reports;
        }

        private void PopulateReportsList()
        {
            foreach (var ship in this.Spaceships)
            {
                var quantity = this.Sales
                    .Where(s => s.ProductName == ship.Model)
                    .Sum(s => s.Quantity);

                var totalSum = this.Sales
                    .Where(s => s.ProductName == ship.Model)
                    .Sum(s => s.Sum);

                var jsonReportEntry = new ProductReport()
                {
                    Id = ship.Id,
                    Model = ship.Model,
                    Category = ship.Category.Name,
                    Price = ship.Price,
                    QuanitySold = quantity,
                    TotalIncome = totalSum
                };

                this.Reports.Add(jsonReportEntry);
            }
        }
    }
}