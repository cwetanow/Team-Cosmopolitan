using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using Ionic.Zip;
using System.Data;
using Factory.ExcelReports.Models;

namespace Factory.ExcelReports
{
    public class ExcelSalesReportsReader
    {
        public void UnzipFiles(string zipFilePath, string unzipedFilesPath)
        {
            if (!File.Exists(zipFilePath))
            {
                // TODO Remove console.writeline with someting better
                System.Console.WriteLine("File not found");
                return;
            }

            if (!Directory.Exists(unzipedFilesPath))
            {
                // TODO Remove console.writeline with someting better
                System.Console.WriteLine("Directory not found");
                return;
            }

            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                foreach (ZipEntry file in zip)
                {
                    if (!File.Exists(unzipedFilesPath + file.FileName))
                    {
                        file.Extract(unzipedFilesPath);
                    }
                }
            }
        }

        public ICollection<ExcelReport> GetSalesReports(string paths)
        {
            if (!Directory.Exists(paths))
            {
                // TODO Remove console.writeline with someting better
                Console.WriteLine("Missing directory");
                return null;
            }

            var reports = new List<ExcelReport>();
            var mainDirectory = Directory.GetDirectories(paths);
            foreach (var dir in mainDirectory)
            {
                var files = Directory.GetFiles(dir);
                foreach (var file in files)
                {
                    var currentReport = GetReport(file);
                    reports.Add(currentReport);
                }
            }

            return reports;
        }

        private ExcelReport GetReport(string filePath)
        {
            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath} ; " +
                "Extended Properties = 'Excel 8.0;HDR=Yes;IMEX=1'; ";

            using (OleDbConnection excelConnection = new OleDbConnection(connectionString))
            {
                excelConnection.Open();
                string sheetName = GetSheetName(excelConnection);
                OleDbCommand cmdReadData = new OleDbCommand($"SELECT *  FROM [{sheetName}]", excelConnection);
                OleDbDataReader reader = cmdReadData.ExecuteReader();

                var report = new ExcelReport();
                int lastSlash = filePath.LastIndexOf(@"\");
                int lastDot = filePath.LastIndexOf(".");
                string date = filePath.Substring(lastSlash + 1, lastDot - lastSlash - 1);
                report.Date = DateTime.Parse(date);

                AddSalesToReport(reader, report);

                return report;
            }
        }

        private void AddSalesToReport(OleDbDataReader reader, ExcelReport report)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    string productName = reader[0].ToString();
                    if (productName != "Total sum")
                    {
                        decimal quantity = decimal.Parse(reader[1].ToString());
                        decimal unitPrice = decimal.Parse(reader[2].ToString());
                        // TODO cannot get the result from the formula.
                        decimal sum = quantity * unitPrice;
                        var sale = new Sale(productName, quantity, unitPrice, sum);
                        report.AddSale(sale);
                    }
                }
            }
        }

        private string GetSheetName(OleDbConnection excelConnection)
        {
            DataTable excelSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();
            return sheetName;
        }
    }
}


