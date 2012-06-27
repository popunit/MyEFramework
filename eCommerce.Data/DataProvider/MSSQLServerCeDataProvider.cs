using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.DataProvider
{
    /// <summary>
    /// Data Provider for MS SQL Server Compact 4 depends on Entity Framework
    /// </summary>
    /// <remarks>
    /// Deploy doc: http://www.cnblogs.com/xiaoweiyu/archive/2011/06/24/2089137.html
    /// </remarks>
    public class MSSQLServerCeDataProvider : EfDataProviderBase
    {
        public override IDbConnectionFactory GetFactory()
        {
            return new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
        }

        public override void SetDatabaseInitializer()
        {
            // TO-DO
            //Database.SetInitializer
        }
    }
}
