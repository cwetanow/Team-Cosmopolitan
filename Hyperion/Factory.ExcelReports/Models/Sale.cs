namespace Factory.ExcelReports.Models
{
    public class Sale
    {
        public Sale(string productName, double quantity, double UnitPrice, double sum)
        {
            this.ProductName = productName;
            this.Quantity = quantity;
            this.UnitPrice = UnitPrice;
            this.Sum = sum;
        }

        //TODO validation
        public string ProductName { get; set; }

        public double Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double Sum { get; set; }
    }
}
