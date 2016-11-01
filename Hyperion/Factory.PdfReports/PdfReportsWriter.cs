using System.Collections.Generic;
using System.IO;
using System.Linq;
using Factory.ExcelReports.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Factory.PdfReports
{
    public class PdfReportsWriter
    {
        private readonly List<ExcelReport> SalesReports;

        public PdfReportsWriter(ICollection<ExcelReport> salesReports)
        {
            this.SalesReports = salesReports.ToList();
        }

        public void WriteAggregatedSalesReportsToPdf(string resultFilePath)
        {
            var pdf = new Document(PageSize.A4, 36, 36, 36, 36);
            var path = File.Create(resultFilePath);
            PdfWriter.GetInstance(pdf, path);
            pdf.Open();

            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            float[] widths = new float[] { 2.5f, 1f, 1.5f, 1.5f };
            table.SetWidths(widths);

            // Header             
            var headerFont = FontFactory.GetFont("Verdana", 14, Font.BOLD);
            var header = new PdfPCell(new Phrase("Aggregated Sales Report - \"Hyperion\" Space Ships", headerFont));
            header.Colspan = 4;
            header.HorizontalAlignment = 1;
            header.Padding = 7;
            table.AddCell(header);

            decimal totalDailySum = 0;
            decimal grandTotal = 0;

            this.SalesReports.Sort((a, b) => a.Date.CompareTo(b.Date));
            foreach (var dailyReport in this.SalesReports)
            {
                // Date
                table.AddCell(GetHeaderCell($"Date: {dailyReport.Date.ToString("dd-MMM-yyyy")}", 0, 4));

                // Table Headers
                table.AddCell(GetHeaderCell("Ship Name"));
                table.AddCell(GetHeaderCell("Quantity"));
                table.AddCell(GetHeaderCell("Unit Price"));
                table.AddCell(GetHeaderCell("Sum"));                

                // Details
                var salesDetails = dailyReport.GetSales();
                foreach (var detailedReport in salesDetails)
                {
                    table.AddCell(GetDetailsCell(detailedReport.ProductName, 0));

                    var quantity = $"{detailedReport.Quantity.ToString()} ships";
                    if (detailedReport.Quantity == 1)
                    {
                        quantity = $"{detailedReport.Quantity.ToString()} ship";
                    }

                    table.AddCell(GetDetailsCell(quantity, 1));

                    table.AddCell(GetDetailsCell($"{detailedReport.UnitPrice.ToString()} BGN", 2));

                    table.AddCell(GetDetailsCell($"{detailedReport.Sum.ToString()} BGN", 2));

                    totalDailySum += detailedReport.Sum;
                }

                // Footer
                table.AddCell(GetFooterCell($"Total sum for {dailyReport.Date.ToString("dd-MMM-yyyy")}", 2, 3));
                table.AddCell(GetFooterCell($"{totalDailySum.ToString()} BGN", 2));

                grandTotal += totalDailySum;
                totalDailySum = 0;
            }

            // Summary
            table.AddCell(GetSummaryCell("Grand Total", 2, 3));
            table.AddCell(GetSummaryCell($"{grandTotal.ToString()} BGN", 2, 1));

            pdf.Add(table);
            pdf.Close();
        }

        private PdfPCell GetHeaderCell(string cellContent, int textAlign = 1, int collSpan = 1)
        {
            var cell = new PdfPCell(new Phrase(cellContent));
            cell.Colspan = collSpan;
            cell.HorizontalAlignment = textAlign;
            cell.Padding = 7;
            cell.BackgroundColor = new BaseColor(24, 158, 156);
            return cell; 
        }

        private PdfPCell GetDetailsCell(string cellContent, int textAlign = 0, int collSpan = 1)
        {
            var cell = new PdfPCell(new Phrase(cellContent));
            cell.Colspan = collSpan;
            cell.HorizontalAlignment = textAlign;
            cell.Padding = 5;
            return cell;
        }

        private PdfPCell GetFooterCell(string cellContent, int textAlign = 2, int collSpan = 1)
        {
            var font = FontFactory.GetFont(BaseFont.HELVETICA, 11, Font.BOLD);
            var cell = new PdfPCell(new Phrase(cellContent, font));
            cell.Colspan = collSpan;
            cell.HorizontalAlignment = textAlign;
            cell.Padding = 5;
            return cell;
        }

        private PdfPCell GetSummaryCell(string cellContent, int textAlign = 2, int collSpan = 1)
        {
            var font = FontFactory.GetFont(BaseFont.HELVETICA, 11, Font.BOLD);
            var cell = new PdfPCell(new Phrase(cellContent, font));
            cell.Colspan = collSpan;
            cell.HorizontalAlignment = textAlign;
            cell.Padding = 5;
            cell.BackgroundColor = new BaseColor(24, 158, 156);
            return cell;
        }
    }
}
