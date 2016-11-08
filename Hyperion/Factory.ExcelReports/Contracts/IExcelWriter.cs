using System.Collections.Generic;

namespace Factory.ExcelReports.Contracts
{
    public interface IExcelWriter
    {
        void WriteRepors(IList<string> headers, IList<string> models, IList<decimal> expensePerModel, IList<decimal> incomePerModel);
    }
}
