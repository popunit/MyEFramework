using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure;

namespace eCommerce.Data
{
    public class EfDataProviderStartUpTask : ITask
    {
        public void Execute()
        {
            var settings = EngineContext.Current.Resolve<DatabaseSettings>();
            if (null != settings && settings.IsValid())
            {
                var provider = EngineContext.Current.Resolve<IEfDataProvider>();
                if (null == provider)
                    throw new Exception("Cannot find Data Provider");
                provider.SetDatabaseInitializer();
            }
        }

        public int Order
        {
            // highest order
            get { return -1; }
        }
    }
}
