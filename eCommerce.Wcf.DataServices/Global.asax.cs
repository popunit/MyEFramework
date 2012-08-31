using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Autofac.Integration.Mvc;
using eCommerce.Core.Caching;
using eCommerce.Core;
using eCommerce.Core.Configuration;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Data;
using eCommerce.Data.DataProvider;
using eCommerce.Data;
using eCommerce.Data.Repositories;
using Autofac.Integration.Wcf;

namespace eCommerce.Wcf.DataServices
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            //System.Reflection.ConstructorInfo constructor = typeof(CommerceDbContext).GetConstructor(Type.EmptyTypes);

            #region Http Context

            builder.Register(context =>
                new HttpContextWrapper(HttpContext.Current) as HttpContextBase);

            builder.Register(context => context.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>().InstancePerHttpRequest();
            builder.Register(context => context.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>().InstancePerHttpRequest();
            builder.Register(context => context.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>().InstancePerHttpRequest();
            builder.Register(context => context.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>().InstancePerHttpRequest();

            #endregion

            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("static_cache_manager").SingleInstance();
            // [TO-DO] InstancePerHttpRequest may not use here because there is not "request" sign
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("per_request_cache_manager").InstancePerHttpRequest();

            // The default ASP.NET and WCF integrations are set up so that InstancePerLifetimeScope() will attach a component to the current web request or service method call.
            // Register WCF services
            //builder.RegisterType<UserService>().InstancePerLifetimeScope();
            //builder.RegisterType<UserExtension>().InstancePerLifetimeScope();

            builder.RegisterType<Config>().InstancePerLifetimeScope();
            builder.RegisterType<WebsiteRoute>().As<IRoute>().InstancePerLifetimeScope();

            // database register
            //builder.RegisterType<CommerceDbContext>().As<IDatabase>().InstancePerLifetimeScope();
            var dbSettingsManager = new DatabaseSettingsManager();
            var databaseSettings = dbSettingsManager.LoadSettings();
            builder.Register(c => dbSettingsManager.LoadSettings()).As<DatabaseSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DatabaseSettings>())).As<IDataProviderManager>().InstancePerDependency();
            // register for two types
            builder.Register(x => (IEfDataProvider)x.Resolve<IDataProviderManager>().DataProvider()).As<IDataProvider>().InstancePerDependency();
            builder.Register(x => (IEfDataProvider)x.Resolve<IDataProviderManager>().DataProvider()).As<IEfDataProvider>().InstancePerDependency();

            if (databaseSettings != null && databaseSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dbSettingsManager.LoadSettings());
                var dataProvider = (IEfDataProvider)efDataProviderManager.DataProvider();
                dataProvider.Init();

                builder.Register<IDatabase>(c => new CommerceDbContext(databaseSettings.DataConnectionString)).InstancePerDependency();
            }
            else
            {
                builder.Register<IDatabase>(c => new CommerceDbContext(dbSettingsManager.LoadSettings().DataConnectionString)).InstancePerDependency();
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