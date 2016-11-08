using System.Linq;
using Factory.MySql.Models;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace Factory.MySql
{
    public class FactoryMySqlDbContext : OpenAccessContext
    {
        private static readonly string ConnectionString = "server=localhost;database=factory;uid=root;pwd=root123";
        private static readonly BackendConfiguration BackendConfig = GetBackendConfiguration();
        private static readonly MetadataSource MetadataSource = GetMetadataSource();

        public FactoryMySqlDbContext()
            : base(ConnectionString, BackendConfig, MetadataSource)
        {
        }

        public IQueryable<MySqlReport> ProductsReports
        {
            get
            {
                return this.GetAll<MySqlReport>();
            }
        }

        public void UpdateDatabase()
        {
            using (var context = new FactoryMySqlDbContext())
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);
            }
        }

        private static void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }

        private static BackendConfiguration GetBackendConfiguration()
        {
            var backendConfig = new BackendConfiguration();
            backendConfig.Backend = "MySql";
            backendConfig.ProviderName = "MySql.Data.MySqlClient";
            return backendConfig;
        }

        private static MetadataSource GetMetadataSource()
        {
            var matadataSource = new FactoryMetadataSource();
            return matadataSource;
        }
    }
}
