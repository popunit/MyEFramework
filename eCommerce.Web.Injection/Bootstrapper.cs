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
using eCommerce.Core.Common;
using eCommerce.Core;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(eCommerce.Web.Injection.Bootstrapper), "Initialize")]
namespace eCommerce.Web.Injection
{
    public class Bootstrapper
    {
        public static void Initialize()
        {
            // initialize MiniProfiler
            // http://www.cnblogs.com/shanyou/archive/2012/04/03/2430977.html
            StackExchange.Profiling.MiniProfilerEF.Initialize();

            Autofac.IContainer builder = (new ContainerBuilder()).Build();
            EngineContext.Initialize(new AutofacContainerManager(builder), new ContainerConfig(), false);

            // dynamicly register IHttpModule
            var routing = EngineContext.Current.Resolve<ISearcher>(typeof(WebsiteSearcher).Name); ;
            routing.RoutingToTypeExecute<IHttpModule>(type => DynamicModuleUtility.RegisterModule(type));

            // dynamicly register as static
            routing.RoutingToExecute<IDeploy>(t => 
            {
                t.RegisterAsSingleton();
                t.Initialize();
            });

        }
    }
}
