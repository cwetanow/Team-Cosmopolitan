namespace Factory.ExcelReports.Models
{
    public class Sale
    {
        public Sale(string productName, int quantity, decimal unitPrice, decimal sum)
        {
            this.ProductName = productName;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.Sum = sum;
        }

        //TODO validation
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Sum { get; set; }
    }
}
