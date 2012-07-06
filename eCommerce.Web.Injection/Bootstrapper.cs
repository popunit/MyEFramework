using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using eCommerce.Core.Configuration;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.IoC;

[assembly: WebActivator.PreApplicationStartMethod(typeof(eCommerce.Web.Injection.Bootstrapper), "Run")]
namespace eCommerce.Web.Injection
{
    public class Bootstrapper
    {
        public static void Run()
        {
            Autofac.IContainer builder = (new ContainerBuilder()).Build();
            EngineContext.Initialize(new AutofacContainerManager(builder), new ContainerConfig(), false);
        }
    }
}
