using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;

namespace eCommerce.Data
{
    public interface IEfDataProvider : IDataProvider
    {
        IDbConnectionFactory GetFactory();
        void SetDatabaseInitializer();
    }
}
