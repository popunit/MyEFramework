using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Wcf;
using eCommerce.Core;
using eCommerce.Data;
using eCommerce.Data.Repositories;

namespace eCommerce.Wcf.IISHost
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>http://www.eidias.com/Blog/2012/2/13/simple-wcf-hosting-wcf-service-by-autofac-in-aspnet-mvc-3</remarks>
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            // The default ASP.NET and WCF integrations are set up so that InstancePerLifetimeScope() will attach a component to the current web request or service method call.
            builder.RegisterType<CommerceDbContext>().As<IDatabase>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}