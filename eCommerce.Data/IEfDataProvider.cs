using eCommerce.Core;
using System.Data.Entity.Infrastructure;

namespace eCommerce.Data
{
    public interface IEfDataProvider : IDataProvider
    {
        IDbConnectionFactory GetFactory();
        void SetDatabaseInitializer();
    }
}
