using Factory.ExcelReports.Models;
using Factory.InsertData.Models.Reports;

namespace Factory.Main
{
    public class DataMigrator
    {
        private static DataMigrator instance;

        private static object lockObject = new object();

        private DataMigrator() { }

        public static DataMigrator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new DataMigrator();
                        }
                    }
                }

                return instance;
            }
        }

        private ProductSale GetProduct(Sale sale)
        {
            var newSale = new ProductSale();
            newSale.Price = sale.UnitPrice;
            newSale.Quantity = sale.Quantity;

        }
    }
}
