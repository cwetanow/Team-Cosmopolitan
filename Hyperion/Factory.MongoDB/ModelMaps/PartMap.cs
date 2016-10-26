namespace Factory.MongoDB.ModelMaps
{
   public class PartMap
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public int Quantity { get; set; }

        public PartTypeMap PartType { get; set; }

        public SupplierMap Supplier { get; set; }
    }
}
