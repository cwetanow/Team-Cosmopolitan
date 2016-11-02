using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Data;
using Factory.ExcelReports.Models;
using Factory.ExcelReports.Contracts;
using Factory.Common.Contracts;

namespace Factory.ExcelReports
{
    public class ExcelsReader : IExcelReader
    {
        private readonly IUserMessageWriter messageWriter;

        public ExcelsReader(IUserMessageWriter messageWriter)
        {
            if (messageWriter == null)
            {
                throw new ArgumentNullException("Message writer must not be null!");
            }

            this.messageWriter = messageWriter;
        }

        public bool AreReportsReaded { get; set; }
        
        public ICollection<ExcelReport> GetReports(string paths)
        {
            if (!Directory.Exists(paths))
            {
                this.messageWriter.Show("Missing directory");
                return null;
            }

            var reports = new List<ExcelReport>();
            var mainDirectory = Directory.GetDirectories(paths);
            foreach (var dir in mainDirectory)
            {
                var files = Directory.GetFiles(dir);
                foreach (var file in files)
                {
                    var currentReport = GetSingleReport(file);
                    reports.Add(currentReport);
                }
            }

            this.AreReportsReaded = true;

            return reports;
        }

        private ExcelReport GetSingleReport(string filePath)
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
                        int quantity = int.Parse(reader[1].ToString());
                        decimal unitPrice = decimal.Parse(reader[2].ToString());
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