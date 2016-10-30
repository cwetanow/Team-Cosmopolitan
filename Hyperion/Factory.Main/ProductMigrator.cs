using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ICollection<Spaceship> GetProductData(ICollection<SpaceshipMap> maps)
        {
            var result = new Collection<Spaceship>();

            foreach (var spaceshipMap in maps)
            {
                var category = new Category
                {
                    Name = spaceshipMap.Category
                };

                var spaceship = new Spaceship
                {
                    Color = spaceshipMap.Color,
                    Model = spaceshipMap.Model,
                    Price = (decimal)spaceshipMap.Price,
                    Year = spaceshipMap.Year,
                    Category = category
                };

                spaceship.Parts = this.GetParts(spaceshipMap.Parts);

                result.Add(spaceship);
            }

            return result;
        }

        private ICollection<Part> GetParts(IList<PartMap> spaceshipMapParts)
        {
            var parts = new Collection<Part>();

            foreach (var mapPart in spaceshipMapParts)
            {
                var country = GetCountry(mapPart.Supplier.Name);

                var city = GetCity(mapPart.Supplier.City, country);

                var supplier = GetSupplier(mapPart.Supplier.Name, mapPart.Supplier.Address, city);

                var part = new Part
                {
                    Name = mapPart.Name,
                    Price = (decimal)mapPart.Price,
                    Quantity = mapPart.Quantity,
                    Supplier = supplier
                };

                parts.Add(part);
            }

            return parts;
        }

        private Supplier GetSupplier(string supplierName, string supplierAddress, City city)
        {
            var supplier = new Supplier
            {
                Address = supplierAddress,
                Name = supplierName,
                City = city
            };

            return supplier;
        }

        private City GetCity(string supplierCity, Country country)
        {
            var city = new City
            {
                Country = country,
                Name = supplierCity
            };

            return city;
        }

        private Country GetCountry(string name)
        {
            var country = new Country
            {
                Name = name
            };

            return country;
        }
    }
}
