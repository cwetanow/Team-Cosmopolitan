using System.Collections.Generic;
using Factory.ExcelReports.Models;

namespace Factory.ExcelReports.Contracts
{
    public interface IExcelReader
    {
        ICollection<ExcelReport> GetReports(string paths);
    }
}
