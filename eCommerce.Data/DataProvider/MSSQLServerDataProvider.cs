using eCommerce.Data.DatabaseInitializers;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace eCommerce.Data.DataProvider
{
    /// <summary>
    /// Data Provider for MS SQL Server depends on Entity Framework
    /// </summary>
    public class MssqlServerDataProvider : EfDataProviderBase
    {
        public override IDbConnectionFactory GetFactory()
        {
            // get sql server factory
            return new SqlConnectionFactory();
        }

        public override void SetDatabaseInitializer()
        {
            // Database setinitializer will excute when dbcontext is initializing,
            // this is a delay execution strategy
            Database.SetInitializer(new CreateTablesIfNotExist<CommerceDbContext>());
        }
    }
}
