using System;
using Factory.MongoDB;
using Factory.MongoDB.ModelMaps;
using System.Collections.Generic;
using Factory.InsertData;
using Factory.ExcelReports;
using Factory.ExcelReports.Models;
using System.Linq;
using System.Diagnostics;

namespace Factory.Main
{
    public class MainModule
    {
        private const string DataName = "spaceships";
        private const string ZipFilePath = "../../../../SalesReports.zip";
        private const string UnzipedFilesPath = "../../../../";
        private const string SalesReportsPath = "../../../../SalesReports";

        public static void Main()
        {

            //  var mongoData = GetDataFromMongoDb();

            // For reading the Excel 2003 files (.xls) use ADO.NET (without ORM or third-party libraries).
            var reports = GetReportsDataFromExcel(ZipFilePath, UnzipedFilesPath);
            foreach (var excelReport in reports)
            {
                Console.WriteLine(excelReport.GetTotalSum() + " " + excelReport.Date);
            }
            // GetDataFromXML();

            var migrator = DataMigrator.Instance;

            //SQL Server should be accessed through Entity Framework.
            //     PopulateSQLDataBase(mongoData);

            //The XML files should be read / written through the standard .NET parsers (by your choice).
            // GenerateXMLReport();

            //For the PDF export use a non-commercial third party framework.
            // GeneratePDFReport();

            //For JSON serializations use a non-commercial library / framework of your choice.
            // GenerateJSONReportsForEachProduct();

            //MySQL should be accessed through Telerik® Data Access ORM (research it).
            // PopulateMySQLDataBaseWithJSONReports();

            //The SQLite embedded database should be accesses though its Entity Framework provider.
            // GetDataFromSQLite();

            //For creating the Excel 2007 files (.xlsx) use a third-party non-commercial library.
            // CreateExcel();
        }

        private static void PopulateSQLDataBase(IEnumerable<SpaceshipMap> data)
        {
            //var dbContext = new FactoryDbContext();
            //dbContext.Spaceships.AddRange(data);

            //dbContext.SaveChanges();
        }

        private static ICollection<ExcelReport> GetReportsDataFromExcel(string zipFilePath, string unzipedFilesPath)
        {
            ExcelSalesReportsReader excelReader = new ExcelSalesReportsReader();
            excelReader.UnzipFiles(zipFilePath, unzipedFilesPath);
            var reports = excelReader.GetSalesReports(SalesReportsPath);

            return reports;
        }

        private static IList<SpaceshipMap> GetDataFromMongoDb()
        {
            var mongoContext = new MongoDBContext(DataName);

            var mongoDBData = mongoContext.GetData();

            return mongoDBData;
        }
    }
}
