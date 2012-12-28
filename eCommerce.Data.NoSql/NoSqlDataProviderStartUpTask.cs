using eCommerce.Core;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure;
using eCommerce.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.NoSql
{
    class NoSqlDataProviderStartUpTask : ITask
    {
        public void Execute()
        {
            var settings = EngineContext.Current.Resolve<DatabaseSettings>();
            if (null != settings && settings.IsValid())
            {
                var provider = EngineContext.Current.Resolve<INoSqlDataProvider>();
                if (null == provider)
                    throw new CommonException("Cannot find Data Provider");
                provider.Init();
            }
        }

        public int Order
        {
            get { return -1; }
        }
    }
}
