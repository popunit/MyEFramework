using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.DataProvider
{
    /// <summary>
    /// Data Provider for MS SQL Server depends on Entity Framework
    /// </summary>
    public class MSSQLServerDataProvider : EfDataProviderBase
    {
        public override IDbConnectionFactory GetFactory()
        {
            // get sql server factory
            return new SqlConnectionFactory();
        }

        public override void SetDatabaseInitializer()
        {
            // TO-DO
            //Database.SetInitializer
        }
    }
}
