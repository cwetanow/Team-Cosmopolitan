using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Factory.InsertData;
using Factory.InsertData.Models.Products;
using Factory.MongoDB.ModelMaps;

namespace Factory.Main
{
    public class ProductMigrator
    {
        private static ProductMigrator instance;

        private static readonly object lockObject = new object();

        private ProductMigrator() { }

        public static ProductMigrator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new ProductMigrator();
                        }
                    }
                }

                return instance;
            }
        }

        public ICollection<Spaceship> GetProductData(ICollection<SpaceshipMap> maps, FactoryDbContext context)
        {
            var categories = this.GetCategories(maps);
            context.Categories.AddRange(categories);
            context.SaveChanges();

            var countries = this.GetCountries(maps);
            context.Countries.AddRange(countries);
            context.SaveChanges();

            var cities = this.GetCities(maps, context.Countries);
            context.Cities.AddRange(cities);
            context.SaveChanges();

            var partTypes = this.GetPartTypes(maps);
            context.PartTypes.AddRange(partTypes);
            context.SaveChanges();

            var suppliers = this.GetSuppliers(maps, context.Cities);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            var parts = this.GetParts(maps, context.Suppliers, context.PartTypes).ToList();
            context.Parts.AddRange(parts);
            context.SaveChanges();

            var products = new Collection<Spaceship>();

            foreach (var spaceshipMap in maps)
            {
                var existingParts = spaceshipMap.Parts.Select(x => new
                {
                    Supplier = x.Supplier.Name,
                    x.Name,
                    Part = x.PartType.Name,
                    Price = (decimal)x.Price,
                    x.Quantity
                });

                var spaceshipParts = context.Parts
                    .Select(x => new
                    {
                        Supplier = x.Supplier.Name,
                        x.Name,
                        Part = x.PartType.Name,
                        x.Price,
                        x.Quantity
                    })
                    .Where(x => existingParts.Contains(x))
                    .Select(x => x.Name);

                var currentShipParts = context.Parts.Where(x => spaceshipParts.Contains(x.Name)).ToList();

                var spaceship = new Spaceship
                {
                    Category = context.Categories.FirstOrDefault(x => x.Name.Equals(spaceshipMap.Category)),
                    Color = spaceshipMap.Color,
                    Model = spaceshipMap.Model,
                    Price = (decimal)spaceshipMap.Price,
                    Year = spaceshipMap.Year,
                    Parts = currentShipParts
                };

                products.Add(spaceship);
            }


            return products;
        }

        private IEnumerable<Part> GetParts(ICollection<SpaceshipMap> maps, IEnumerable<Supplier> suppliers, IEnumerable<PartType> types)
        {
            var parts = maps.SelectMany(x => x.Parts)
                .Select(x => new
                {
                    Supplier = x.Supplier.Name,
                    PartType = x.PartType.Name,
                    x.Name,
                    x.Price,
                    x.Quantity
                })
                .Distinct()
                .Select(x => new Part
                {
                    Name = x.Name,
                    PartType = types.FirstOrDefault(t => t.Name.Equals(x.PartType)),
                    Price = (decimal)x.Price,
                    Quantity = x.Quantity,
                    Supplier = suppliers.FirstOrDefault(s => s.Name.Equals(x.Supplier))
                });

            return parts;
        }

        private IEnumerable<PartType> GetPartTypes(ICollection<SpaceshipMap> maps)
        {
            var types = maps.SelectMany(x => x.Parts)
                .Select(x => x.PartType)
                .Select(x => x.Name)
                .Distinct()
                .Select(x => new PartType
                {
                    Name = x
                });

            return types;
        }

        private IEnumerable<City> GetCities(ICollection<SpaceshipMap> maps, IEnumerable<Country> countries)
        {
            var cities = maps
                .SelectMany(x => x.Parts)
                .Select(x => new
                {
                    City = x.Supplier.City,
                    Country = x.Supplier.Country
                })
                .Distinct()
                .Select(x => new City
                {
                    Country = countries.FirstOrDefault(c => c.Name.Equals(x.Country)),
                    Name = x.City
                });

            return cities;
        }

        private IEnumerable<Country> GetCountries(ICollection<SpaceshipMap> maps)
        {
            var countriesNames = maps
                .SelectMany(x => x.Parts)
                .Select(x => x.Supplier.Country)
                .Distinct()
                .Select(x => new Country
                {
                    Name = x
                });

            return countriesNames;
        }

        private ICollection<Category> GetCategories(IEnumerable<SpaceshipMap> ships)
        {
            var categoriesNames = ships.Select(c => c.Category)
                .Distinct()
                .Select(category => new Category
                {
                    Name = category
                })
                .ToList();

            return categoriesNames;
        }

        private IEnumerable<Supplier> GetSuppliers(IEnumerable<SpaceshipMap> maps, IEnumerable<City> cities)
        {
            var suppliers = maps.SelectMany(x => x.Parts)
                .Select(x => new
                {
                    x.Supplier.Name,
                    x.Supplier.Address,
                    x.Supplier.City
                }).Distinct()
                .Select(x => new Supplier
                {
                    Name = x.Name,
                    Address = x.Address,
                    City = cities.FirstOrDefault(c => c.Name.EndsWith(x.City))
                });

            return suppliers;
        }
    }
}
