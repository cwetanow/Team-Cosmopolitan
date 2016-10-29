namespace Factory.ExcelReports.Models
{
    public class Sale
    {
        public Sale(string productName, decimal quantity, decimal UnitPrice, decimal sum)
        {
            this.ProductName = productName;
            this.Quantity = quantity;
            this.UnitPrice = UnitPrice;
            this.Sum = sum;
        }

        //TODO validation
        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Sum { get; set; }
    }
}
