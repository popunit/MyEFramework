using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Wcf;
using Autofac.Integration.Mvc;
using eCommerce.Core;
using eCommerce.Core.Configuration;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure;
using eCommerce.Data;
using eCommerce.Data.DataProvider;
using eCommerce.Data.Repositories;
using eCommerce.Wcf.Services.Users;
using eCommerce.Core.Caching;
using eCommerce.Data.Domain.Common;
using eCommerce.Wcf.Services.Common;

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

            #region Http Context

            //builder.Register(context =>
            //    new HttpContextWrapper(HttpContext.Current) as HttpContextBase);

            //builder.Register(context => context.Resolve<HttpContextBase>().Request)
            //    .As<HttpRequestBase>().InstancePerHttpRequest();
            //builder.Register(context => context.Resolve<HttpContextBase>().Response)
            //    .As<HttpResponseBase>().InstancePerHttpRequest();
            //builder.Register(context => context.Resolve<HttpContextBase>().Server)
            //    .As<HttpServerUtilityBase>().InstancePerHttpRequest();
            //builder.Register(context => context.Resolve<HttpContextBase>().Session)
            //    .As<HttpSessionStateBase>().InstancePerHttpRequest();

            #endregion

            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("static_cache_manager").SingleInstance();
            // [TO-DO] InstancePerHttpRequest may not use here because there is not "request" sign
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("per_request_cache_manager").InstancePerHttpRequest();

            // Since WCF requests does not have an http context we can not use InstancePerHttpRequest(). Instead we can use InstancePerLifetimeScope() 
            // which is resolvable for both WCF and http requests. Autofac Wiki says :
            // The default ASP.NET and WCF integrations are set up so that InstancePerLifetimeScope() will attach a component to the current web request or service method call.
            // Register WCF services
            builder.RegisterType<UserService>().InstancePerLifetimeScope();
            builder.RegisterType<GenericCharacteristicService>().InstancePerLifetimeScope();
            builder.RegisterType<DataInfoService>().InstancePerLifetimeScope();

            builder.RegisterType<Config>().InstancePerLifetimeScope();
            builder.RegisterType<WebsiteSearcher>().As<ISearcher>().InstancePerLifetimeScope();

            // database register
            //builder.RegisterType<CommerceDbContext>().As<IDatabase>().InstancePerLifetimeScope();
            var dbSettingsManager = new DatabaseSettingsManager();
            var databaseSettings = dbSettingsManager.LoadSettings();
            builder.Register(context => dbSettingsManager.LoadSettings()).As<DatabaseSettings>();
            builder.Register(context => new EfDataProviderManager(context.Resolve<DatabaseSettings>())).As<IDataProviderManager>().InstancePerDependency();
            // register for two types
            builder.Register(context => (IEfDataProvider)context.Resolve<IDataProviderManager>().DataProvider()).As<IDataProvider>().InstancePerDependency();
            builder.Register(context => (IEfDataProvider)context.Resolve<IDataProviderManager>().DataProvider()).As<IEfDataProvider>().InstancePerDependency();

            if (databaseSettings != null && databaseSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dbSettingsManager.LoadSettings());
                var dataProvider = (IEfDataProvider)efDataProviderManager.DataProvider();
                dataProvider.Init();

                builder.Register<IDatabase>(context => new CommerceDbContext(databaseSettings.DataConnectionString)).InstancePerLifetimeScope();
            }
            else
            {
                builder.Register<IDatabase>(context => new CommerceDbContext(dbSettingsManager.LoadSettings().DataConnectionString)).InstancePerLifetimeScope();
            }

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