using System.Collections.Generic;
using System.Linq;

namespace Factory.SQLite
{
    public class ReadSQLiteDB
    {
        public IDictionary<string, decimal> GetModelExpensesData()
        {
            var sqlDb = new ExpencesEntities();
            var modelExpensesPair = new Dictionary<string, decimal>();
            var models = sqlDb.Expenses.Select(x => x.ProductModel).Distinct().ToList();
            foreach (var model in models)
            {
                var expensesPerModel = sqlDb.Expenses.Where(x => x.ProductModel == model).Sum(x => x.Expense);
                modelExpensesPair.Add(model, (decimal)expensesPerModel);
            }

            return modelExpensesPair;
        }
    }
}
