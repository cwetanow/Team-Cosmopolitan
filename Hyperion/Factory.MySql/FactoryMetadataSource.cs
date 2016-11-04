using System.Collections.Generic;
using Factory.MySql.Models;
using Telerik.OpenAccess.Metadata.Fluent;

namespace Factory.MySql
{
    public class FactoryMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations =
                new List<MappingConfiguration>();

            var reportMapping = new MappingConfiguration<MySqlReport>();
            reportMapping.MapType(report => new
            {
                id = report.Id,
                model = report.Model,
                category = report.Category,
                price = report.Price,
                quantity_sold = report.QuanitySold,
                total_income = report.TotalIncome
            }).ToTable("product_reports");
            reportMapping.HasProperty(report => report.Id).IsIdentity();

            configurations.Add(reportMapping);

            return configurations;
        }
    }
}