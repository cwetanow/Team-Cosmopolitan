using System.Collections.Generic;

namespace Factory.ExcelReports.Contracts
{
    public interface IExcelWriter
    {
        void WriteRepors(IList<string> headers, IDictionary<string, IList<int>> modelFinancialData);
    }
}
