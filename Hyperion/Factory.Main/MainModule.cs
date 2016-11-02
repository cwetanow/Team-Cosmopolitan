using System.Collections.Generic;
using System.Linq;
using Factory.ExcelReports;
using Factory.ExcelReports.Models;
using Factory.InsertData;
using Factory.InsertData.Models.Products;
using Factory.InsertData.Models.Reports;
using Factory.JsonReports;
using Factory.MongoDB;
using Factory.MongoDB.ModelMaps;
using Factory.PdfReports;
using Factory.LoadXML;
using Factory.SQLite;
using Factory.XmlReports;
using Factory.Common;
using MongoDB.Driver;

namespace Factory.Main
{
    public class MainModule
    {
        public static void Main()
        {
            var context = new FactoryDbContext();
            context.Database.CreateIfNotExists();

            var mongoData = GetDataFromMongoDb(Constants.DataName, Constants.CollectionName);

            // For reading the Excel 2003 files (.xls) use ADO.NET (without ORM or third-party libraries).
            var reports = GetReportsDataFromExcel(Constants.ZipFilePath, Constants.UnzipedFilesPath);

            //GetDataFromXML();

            //SQL Server should be accessed through Entity Framework.
            var productData = ProductMigrator.Instance.GetProductData(mongoData, context);
            PopulateSQLDataBase(productData, context);

            var reportsData = ReportMigrator.Instance.GetReports(reports);
            PopulateSqlDbReports(reportsData, context);

            //The XML files should be read / written through the standard .NET parsers (by your choice).
            GenerateXMLReport(context, Constants.XmlReportsPath);

            //For the PDF export use a non-commercial third party framework.
            GeneratePDFReport(context, Constants.PdfReportsPath);

            //For JSON serializations use a non-commercial library / framework of your choice.
            GenerateJSONReports(context, Constants.JsonReportsPath);

            //MySQL should be accessed through Telerik® Data Access ORM (research it).
            // PopulateMySQLDataBase();

            //The SQLite embedded database should be accesses though its Entity Framework provider.
            var modelExpensesPair = GetDataFromSQLite();

            //For creating the Excel 2007 files (.xlsx) use a third-party non-commercial library.
            //  CreateExcelYearlyFinancialResult();
        }

        private static void GenerateJSONReports(FactoryDbContext context, string resultFilesPath)
        {
            var spaceships = context.Spaceships.ToList();
            var jsonWriter = new JsonReportsWriter(spaceships);
            jsonWriter.WriteReportsToJson(resultFilesPath);
        }

        private static IDictionary<string, decimal> GetDataFromSQLite()
        {
            ReadSQLiteDB sqlDB = new ReadSQLiteDB();
            var modelExpesesPair = sqlDB.GetModelExpensesData();

            return modelExpesesPair;
        }

        private static void GenerateXMLReport(FactoryDbContext context, string resultFilePath)
        {
            var shipModels = context.Spaceships.Select(sh => sh.Model).ToList();
            var reports = context.Reports.OrderBy(r => r.Date).ToList();
            var xmlWriter = new XmlReportsWriter(shipModels, reports);
            xmlWriter.WriteReportsToXml(resultFilePath);
        }

        private static void GeneratePDFReport(FactoryDbContext context, string resultFilePath)
        {
            var salesReports = context.Reports.OrderBy(r => r.Date).ToList();
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
            var userMessanger = new UserMessageWriter();

            UzipFiles unzipper = new UzipFiles(userMessanger);
            unzipper.Unzip(zipFilePath, unzipedFilesPath);

            ExcelsReader excelReader = new ExcelsReader(userMessanger);
            var salesReports = excelReader.GetReports(Constants.SalesReportsPath);

            return salesReports;
        }

        private static IList<SpaceshipMap> GetDataFromMongoDb(string dataName, string collectionName)
        {
            var mongoContext = new MongoDBContext(dataName, Constants.MongoDbConnectionString);
            var mongoDBData = mongoContext.GetData(collectionName);

            return mongoDBData;
        }

        private static void GetDataFromXML()
        {
            var mongo = new MongoDBContext(Constants.DataName, Constants.MongoDbConnectionString);
            var importer = new FactoryXmlImporter();
            importer.ImportDataFromXml((IMongoDatabase)mongo, Constants.XmlDataToImport);
        }
    }
}
