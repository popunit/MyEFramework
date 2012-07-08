using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Mvc;
using eCommerce.Core;
using eCommerce.Services;

namespace eCommerce.Web.Injection
{
    public class WebDependencyRegistrar : RegistrarBase<ContainerBuilder>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="route"></param>
        /// <remarks>HttpContext is always available here</remarks>
        public override void Register(ContainerBuilder builder, Core.Infrastructure.IRoute route)
        {
            builder.RegisterType<MobileDeviceCheck>().As<IMobileDeviceCheck>().InstancePerHttpRequest().Keyed<IMobileDeviceCheck>(typeof(MobileDeviceCheck));
        }

        public override int Order
        {
            get { return 0; }
        }
    }
}
