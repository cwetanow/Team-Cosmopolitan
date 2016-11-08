# Team-Cosmopolitan

## Practical Teamwork Project
The central idea behind this team work project was to make an application that uses relational and non relational types of databases (Sql, MySql, SQLite and MongoDb), read and write various files (XML, PDF, Json, Excel). More precisely, the assignment was to design, develop and test a C# application for importing Excel reports from a ZIP file and the product data from MongoDB into SQL Server, generate XML reports and PDF reports, create reports as JSON documents and also load them into MySQL, load additional information by our choice from XML file, read other information by our choice from SQLite and calculate aggregated results and write them into Excel file.

## Images
![project](https://cloud.githubusercontent.com/assets/3619393/10135708/e4b681d4-65f8-11e5-9243-0aea8fa008eb.png)

## Problems
-  Load Excel Reports from ZIP File and data from MongoDb. Create Sql database and populate it with the data from the excel and MongoDb.
-  Generate PDF Reports related to the imported data in Sql
-  Generate XML Report related to the imported data in Sql
-  Generate JSON Reports related to the imported data in Sql
-  Load additional data from XML and import it into the Sql database and the MongoDB one
-  Populate some data from Sql to MySql
-  Load the data from MySql together with another data from SQLite and write it into excel file

## Additional Requirements
*	Your main program logic should be a C# application (a set of modules, executed sequentially one after another).
*	Use non-commercial library to read the ZIP file.
*	For reading the Excel 2003 files (.xls) use ADO.NET (without ORM or third-party libraries).
*	MySQL should be accessed through Telerik® Data Access ORM (research it).
*	SQL Server should be accessed through Entity Framework.
*	You are free to use "code first" or "database first" approach or both for the ORM frameworks.
*	For the PDF export use a non-commercial third party framework.
*	The XML files should be read / written through the standard .NET parsers (by your choice).
*	For JSON serializations use a non-commercial library / framework of your choice.
*	MongoDB should be accessed through the Official MongoDB C# Driver.
*	The SQLite embedded database should be accesses though its Entity Framework provider.
*	For creating the Excel 2007 files (.xlsx) use a third-party non-commercial library.
