using System;
using Factory.MongoDB;
using Factory.MongoDB.ModelMaps;
using System.Collections.Generic;

namespace Factory.Main
{
    public class MainModule
    {
        private static List<SpaceshipMap> mongoDBData;
        private const string DataName = "spaceships";

        public static void Main(string[] args)
        {

            GetDataFromMongoDb();
            foreach (var item in mongoDBData)
            {
                Console.WriteLine(item.Model);
            }
            // For reading the Excel 2003 files (.xls) use ADO.NET (without ORM or third-party libraries).
            // GetReportsDataFromExcel();

            // GetDataFromXML();

            //SQL Server should be accessed through Entity Framework.
            // PopulateSQLDataBase();

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

        private static void GetDataFromMongoDb()
        {
            var mongoContext = new MongoDBContext(DataName);

            mongoDBData = mongoContext.GetData();
        }
    }
}
