using eCommerce.Wcf.Services.Common;
using eCommerce.Wcf.Services.Contracts.Common;
using eCommerce.Wcf.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace eCommerce.Wcf.IISHost
{
    [assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Bootstrapper), "Initialize")]
    public class Bootstrapper
    {
        public static void Initialize()
        {
            // use file-less without extension(.svc)
            RouteTable.Routes.Add(new ServiceRoute(
                "Common/datainfoservice",
                new AutofacServiceHostDiscoveryFactory(),
                typeof(DataInfoService)));

            RouteTable.Routes.Add(new ServiceRoute(
                "Users/userservice",
                new AutofacServiceHostDiscoveryFactory(),
                typeof(UserService)));

            RouteTable.Routes.Add(new ServiceRoute(
                "Common/genericcharacteristicService",
                new AutofacServiceHostDiscoveryFactory(),
                typeof(GenericCharacteristicService)));
        }
    }
}