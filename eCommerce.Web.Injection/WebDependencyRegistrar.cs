﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            // register call back action (main for construction of type)
            // if httpcontext == null 
            // reason: 1.async request (hack)
            //         2. not web application (test)
            builder.Register(context =>
                null != HttpContext.Current ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                MvcMocker.FakeHttpContext());

            builder.RegisterType<MobileDeviceCheck>().As<IMobileDeviceCheck>().InstancePerHttpRequest().Keyed<IMobileDeviceCheck>(typeof(MobileDeviceCheck));
        }

        public override int Order
        {
            get { return 0; }
        }
    }
}
