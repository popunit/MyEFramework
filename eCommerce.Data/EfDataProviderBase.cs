using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;

namespace eCommerce.Data
{
    /// <summary>
    /// Data Provider based on Entity Framework
    /// </summary>
    public abstract class EfDataProviderBase : IDataProvider
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
