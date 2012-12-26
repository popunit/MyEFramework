using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.Wcf;
using Autofac;
using eCommerce.Core;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure;
using eCommerce.Exception;

namespace eCommerce.Data
{
    public class EfDataProviderStartUpTask : ITask
    {
        public void Execute()
        {
            var settings = EngineContext.Current.Resolve<DatabaseSettings>();
            //var settings = AutofacHostFactory.Container.Resolve<DatabaseSettings>();
            if (null != settings && settings.IsValid())
            {
                var provider = EngineContext.Current.Resolve<IEfDataProvider>();
                //var provider = AutofacHostFactory.Container.Resolve<IEfDataProvider>();
                if (null == provider)
                    throw new CommonException("Cannot find Data Provider");
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
