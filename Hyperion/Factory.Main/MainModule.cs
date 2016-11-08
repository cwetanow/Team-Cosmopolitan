using System.Collections.Generic;
using System.Linq;
using Factory.Common;
using Factory.ExcelReports;
using Factory.ExcelReports.Models;
using Factory.InsertData;
using Factory.InsertData.Models.Products;
using Factory.InsertData.Models.Reports;
using Factory.InsertData.Models.SpaceshipMissions;
using Factory.JsonReports;
using Factory.LoadXML;
using Factory.LoadXML.Models;
using Factory.Models;
using Factory.MongoDB;
using Factory.MongoDB.ModelMaps;
using Factory.MySql;
using Factory.MySql.Models;
using Factory.PdfReports;
using Factory.SQLite;
using Factory.XmlReports;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Factory.Main
{
    public class MainModule
    {
        public static void Main()
        {
            var context = new FactoryDbContext();
            context.Database.CreateIfNotExists();

            var mongoData = GetDataFromMongoDb(Constants.DataName, Constants.CollectionName);
            var reports = GetReportsDataFromExcel(Constants.ZipFilePath, Constants.UnzipedFilesPath);
            
            var productData = ProductMigrator.Instance.GetProductData(mongoData, context);
            PopulateSQLDbWithProducts(productData, context);

            var reportsData = ReportMigrator.Instance.GetReports(reports);
            PopulateSqlDbWithReports(reportsData, context);

            ImportXmlToMongoDb();
            ImportXMLToSqlServer();

            GenerateXMLReport(context, Constants.XmlReportsPath);
            GeneratePDFReport(context, Constants.PdfReportsPath);
            GenerateJSONReports(context, Constants.JsonReportsPath);

            var mySqlContext = new FactoryMySqlDbContext();
            mySqlContext.UpdateDatabase();
            PopulateMySQLDataBase(context, mySqlContext);

            var expensesPerModel = GetDataFromSQLite();
            var incomesPerModel = GetIncomePerModel(mySqlContext);
            CreateExcelYearlyFinancialResult(expensesPerModel, incomesPerModel);
        }

        private static void CreateExcelYearlyFinancialResult(IDictionary<string, decimal> expensesPerModel, IDictionary<string, decimal> incomesPerModel)
        {
            var headers = new List<string>() { "Model", "Incomes", "Expenses", "Financial Result" };
            var expenses = expensesPerModel.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            var incomes = incomesPerModel.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            var models = expensesPerModel.OrderBy(x => x.Key).Select(x => x.Key).ToList();

            var excelWriter = new ExcelWriter();
            excelWriter.WriteRepors(headers, models, expenses, incomes);
        }

        private static IDictionary<string, decimal> GetIncomePerModel(FactoryMySqlDbContext mySqlContext)
        {
            var dict = new Dictionary<string, decimal>();
            mySqlContext
                .ProductsReports
                .ToList()
                .ForEach(r =>
                {
                    dict.Add(r.Model, r.TotalIncome);
                });

            return dict;
        }

        private static void PopulateMySQLDataBase(FactoryDbContext sqlContext, FactoryMySqlDbContext mySqlContext)
        {
            var spaceships = sqlContext.Spaceships.ToList();
            var sales = sqlContext.Sales.ToList();
            var reports = new List<ProductReport>();
            var jsonWriter = new JsonReportsWriter(spaceships, sales, reports);
            var jsonData = jsonWriter.GetReportsInJsonFormat();

            if (mySqlContext.ProductsReports.Count() == 0)
            {
                foreach (var json in jsonData)
                {
                    var report = JsonConvert.DeserializeObject<MySqlReport>(json);
                    mySqlContext.Add(report);
                }

                mySqlContext.SaveChanges();
            }
        }

        private static void GenerateJSONReports(FactoryDbContext context, string resultFilesPath)
        {
            var spaceships = context.Spaceships.ToList();
            var sales = context.Sales.ToList();
            var reports = new List<ProductReport>();
            var jsonWriter = new JsonReportsWriter(spaceships, sales, reports);
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

        private static void PopulateSQLDbWithProducts(IEnumerable<Spaceship> productData, FactoryDbContext context)
        {
            context.Spaceships.AddRange(productData);
            context.SaveChanges();
        }

        private static void PopulateSqlDbWithReports(IEnumerable<Report> reportsForSql, FactoryDbContext context)
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

        private static void ImportXmlToMongoDb()
        {
            var mongo = new MongoClient(Constants.MongoDbConnectionString);
            var db = mongo.GetDatabase(Constants.DataName);

            var collection = FactoryXmlImporter.ImportSpaceships(Constants.XmlDataToImport);

            var mongoCollection = db.GetCollection<SpaceshipMissionsXmlModel>("missions");

            foreach (var spaceship in collection)
            {
                mongoCollection.InsertOne(spaceship);
            }
        }

        private static void ImportXMLToSqlServer()
        {
            var db = new FactoryDbContext();

            var collection = FactoryXmlImporter.ImportSpaceships(Constants.XmlDataToImport);

            foreach (var spaceship in collection)
            {
                var sp = new SpaceshipMission
                {
                    SpaceshipName = spaceship.SpaceshipName,
                    Captain = spaceship.Captain,
                    HomePlanet = spaceship.HomePlanet,
                    NumberOfCrewMembers = spaceship.NumberOfCrewMembers,
                    MissionType = spaceship.MissionType,
                    Commision = spaceship.Commission,
                    MissionStatus = spaceship.MissionStatus
                };

                db.SpaceshipMissions.Add(sp);

                db.SaveChanges();
                db = new FactoryDbContext();
            }

            db.SaveChanges();
        }
    }
}