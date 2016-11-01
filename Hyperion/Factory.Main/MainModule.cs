using System.Collections.Generic;
using Factory.ExcelReports;
using Factory.ExcelReports.Models;
using Factory.InsertData;
using Factory.InsertData.Models.Products;
using Factory.InsertData.Models.Reports;
using Factory.MongoDB;
using Factory.MongoDB.ModelMaps;
using Factory.PdfReports;

namespace Factory.Main
{
    public class MainModule
    {
        private const string DataName = "spaceships";
        private const string ZipFilePath = "../../../../SalesReports.zip";
        private const string UnzipedFilesPath = "../../../../";
        private const string SalesReportsPath = "../../../../SalesReports";
        private const string PdfReportsPath = "../../../../PdfReports/AggregatedSalesReport.pdf";

        public static void Main()
        {
            var context = new FactoryDbContext();
            context.Database.CreateIfNotExists();

            var mongoData = GetDataFromMongoDb();

            // For reading the Excel 2003 files (.xls) use ADO.NET (without ORM or third-party libraries).
            var reports = GetReportsDataFromExcel(ZipFilePath, UnzipedFilesPath);

            // GetDataFromXML();

            //SQL Server should be accessed through Entity Framework.
            //     PopulateSQLDataBase(mongoData);
            var productData = ProductMigrator.Instance.GetProductData(mongoData, context);
            PopulateSQLDataBase(productData, context);

            var reportsData = ReportMigrator.Instance.GetReports(reports);
            PopulateSqlDbReports(reportsData, context);

            //The XML files should be read / written through the standard .NET parsers (by your choice).
            // GenerateXMLReport();

            //For the PDF export use a non-commercial third party framework.
            System.Console.WriteLine(1);
            GeneratePDFReport(reports, PdfReportsPath);

            //For JSON serializations use a non-commercial library / framework of your choice.
            // GenerateJSONReportsForEachProduct();

            //MySQL should be accessed through Telerik® Data Access ORM (research it).
            // PopulateMySQLDataBaseWithJSONReports();

            //The SQLite embedded database should be accesses though its Entity Framework provider.
            // GetDataFromSQLite();

            //For creating the Excel 2007 files (.xlsx) use a third-party non-commercial library.
            // CreateExcel();
        }

        private static void GeneratePDFReport(ICollection<ExcelReport> salesReports, string resultFilePath)
        {
            var pdfWriter = new PdfReportsWriter(salesReports);
            pdfWriter.WriteAggregatedSalesReportsToPdf(resultFilePath);
        }

        private static void PopulateSQLDataBase(IEnumerable<Spaceship> productData, FactoryDbContext context)
        {
            context.Spaceships.AddRange(productData);
            context.SaveChanges();
        }

        private static void PopulateSqlDbReports(IEnumerable<Report> reportsForSql, FactoryDbContext context)
        {
            context.Reports.AddRange(reportsForSql);
            context.SaveChanges();
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
