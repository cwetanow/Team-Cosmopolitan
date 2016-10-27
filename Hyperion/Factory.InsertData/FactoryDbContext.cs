using System.Data.Entity;
using Factory.MongoDB.ModelMaps;

namespace Factory.InsertData
{
    public class FactoryDbContext : DbContext
    {
        public FactoryDbContext()
            : base("Factory")
        {
        }

        public DbSet<PartMap> Parts { get; set; }
        public DbSet<PartTypeMap> PartTypes { get; set; }
        public DbSet<SpaceshipMap> Spaceships { get; set; }
        public DbSet<SupplierMap> Suppliers { get; set; }
    }
}
