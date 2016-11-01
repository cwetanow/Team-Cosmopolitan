using System.Data.Entity;
using Factory.InsertData.Migrations;
using Factory.InsertData.Models.Products;
using Factory.InsertData.Models.Reports;
using Factory.MongoDB.ModelMaps;

namespace Factory.InsertData
{
    public class FactoryDbContext : DbContext
    {
        public FactoryDbContext()
            : base("Factory")
        {
        }

        public DbSet<Part> Parts { get; set; }

        public DbSet<PartType> PartTypes { get; set; }

        public DbSet<Spaceship> Spaceships { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<ProductSale> Sales { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FactoryDbContext, Configuration>());
            base.OnModelCreating(modelBuilder);
        }
    }
}

