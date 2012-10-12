using Autofac.Integration.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using ServiceModelEx;

namespace eCommerce.Wcf.IISHost
{
    public class AutofacServiceHostDiscoveryFactory : AutofacServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);
            //host.EnableDiscovery();
            return host;
        }
    }
}