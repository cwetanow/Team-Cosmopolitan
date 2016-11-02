using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Factory.InsertData.Models.Reports;

namespace Factory.XmlReports
{
    public class XmlReportsWriter
    {
        private readonly ICollection<string> ShipModels;
        private readonly ICollection<Report> Reports;

        public XmlReportsWriter(ICollection<string> shipModels, ICollection<Report> reports)
        {
            this.ShipModels = shipModels;
            this.Reports = reports;
        }

        public void WriteReportsToXml(string resultFilePath)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            using (var doc = XmlWriter.Create(resultFilePath, settings))
            {
                doc.WriteStartDocument();
                doc.WriteStartElement("sales");
                doc.WriteAttributeString("year", "4500");

                foreach (var model in this.ShipModels)
                {
                    doc.WriteStartElement("sale");
                    doc.WriteAttributeString("model", model);

                    GetAggregatedDataFromReports(doc, model);

                    doc.WriteEndElement();
                }

                doc.WriteEndElement();
                doc.WriteEndDocument();
            }

            RemoveEmptyEntries(resultFilePath);
        }

        private void GetAggregatedDataFromReports(XmlWriter doc, string model)
        {
            int unitsSold = 0;
            decimal totalSum = 0;
            int currentMonth = 1;
            foreach (var report in this.Reports)
            {
                foreach (var detailedReport in report.Sales)
                {
                    if (detailedReport.ProductName == model)
                    {
                        totalSum += detailedReport.Sum;
                        unitsSold += detailedReport.Quantity;
                        break;
                    }
                }

                if (currentMonth != report.Date.AddDays(1).Month && totalSum != 0)
                {
                    doc.WriteStartElement("summary");
                    doc.WriteAttributeString("month", report.Date.ToString("MMMM"));
                    doc.WriteAttributeString("units-sold", unitsSold.ToString());
                    doc.WriteAttributeString("total-sum", totalSum.ToString());
                    doc.WriteEndElement();

                    currentMonth = report.Date.AddDays(1).Month;
                    totalSum = 0;
                    unitsSold = 0;
                }
            }
        }

        private void RemoveEmptyEntries(string fileUrl)
        {
            var doc = XDocument.Load(fileUrl);
            doc.Descendants("sale")
                .Where(x => !x.HasElements)
                .Remove();

            doc.Save(fileUrl);
        }
    }
}