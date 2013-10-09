using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace eCommerce.Data
{
    /// <summary>
    /// Data Provider based on Entity Framework
    /// </summary>
    public abstract class EfDataProviderBase : IEfDataProvider
    {
        public abstract IDbConnectionFactory GetFactory();
        public abstract void SetDatabaseInitializer();

        public virtual void Init()
        {
            Database.DefaultConnectionFactory = GetFactory(); // set base factory
            SetDatabaseInitializer();
        }
    }
}
