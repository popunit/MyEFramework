using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using eCommerce.Core.Configuration;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.IoC;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivator.PreApplicationStartMethod(typeof(eCommerce.Web.Injection.Bootstrapper), "Run")]
namespace eCommerce.Web.Injection
{
    public class Bootstrapper
    {
        public static void Run()
        {
            // initialize MiniProfiler
            // http://www.cnblogs.com/shanyou/archive/2012/04/03/2430977.html
            StackExchange.Profiling.MiniProfilerEF.Initialize();

            Autofac.IContainer builder = (new ContainerBuilder()).Build();
            EngineContext.Initialize(new AutofacContainerManager(builder), new ContainerConfig(), false);

            // dynamicly register IHttpModule
            var routing = EngineContext.Current.Resolve<IRoute>(typeof(WebsiteRoute).Name);
            RouteHelper.RoutingToType<IHttpModule>(routing, type => DynamicModuleUtility.RegisterModule(type));
        }
    }
}
