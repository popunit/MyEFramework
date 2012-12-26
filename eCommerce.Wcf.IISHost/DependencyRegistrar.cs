using Autofac;
using Autofac.Integration.Wcf;
using eCommerce.Core;
using eCommerce.Core.Caching;
using eCommerce.Core.Configuration;
using eCommerce.Core.Data;
using eCommerce.Core.Diagnosis;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.IoC;
using eCommerce.Data;
using eCommerce.Data.DataProvider;
using eCommerce.Data.Repositories;
using eCommerce.Wcf.Services.Common;
using eCommerce.Wcf.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Wcf.IISHost
{
    public static class DependencyRegistrar
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("static_cache_manager").SingleInstance();
            //builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("per_request_cache_manager").InstancePerLifetimeScope();

            // Since WCF requests does not have an http context we can not use InstancePerHttpRequest(). Instead we can use InstancePerLifetimeScope() 
            // which is resolvable for both WCF and http requests. Autofac Wiki says :
            // The default ASP.NET and WCF integrations are set up so that InstancePerLifetimeScope() will attach a component to the current web request or service method call.
            // Register WCF services
            builder.RegisterType<UserService>().InstancePerLifetimeScope();
            builder.RegisterType<GenericCharacteristicService>().InstancePerLifetimeScope();
            builder.RegisterType<DataInfoService>().InstancePerLifetimeScope();

            builder.RegisterType<Config>().InstancePerLifetimeScope();
            builder.RegisterType<WebsiteSearcher>().As<ISearcher>().InstancePerLifetimeScope();
            builder.RegisterType<DebugHelper>().As<IDebugHelper>().SingleInstance();

            // database register
            var dbSettingsManager = new DatabaseSettingsManager();
            var databaseSettings = dbSettingsManager.LoadSettings();
            builder.RegisterType<DatabaseSettingsManager>().InstancePerDependency();
            builder.Register(context => context.Resolve<DatabaseSettingsManager>().LoadSettings()).As<DatabaseSettings>().InstancePerLifetimeScope();
            builder.Register(context => new EfDataProviderManager(context.Resolve<DatabaseSettings>())).As<IDataProviderManager>().InstancePerDependency();
            // register for two types
            builder.Register(context => (IEfDataProvider)context.Resolve<IDataProviderManager>().DataProvider()).As<IDataProvider>().InstancePerDependency();
            builder.Register(context => (IEfDataProvider)context.Resolve<IDataProviderManager>().DataProvider()).As<IEfDataProvider>().InstancePerDependency();

            if (databaseSettings != null && databaseSettings.IsValid())
            {
                //var efDataProviderManager = new EfDataProviderManager(dbSettingsManager.LoadSettings());
                //var dataProvider = (IEfDataProvider)efDataProviderManager.DataProvider();
                //dataProvider.Init();

                builder.Register<IDatabase>(context => new CommerceDbContext(databaseSettings.DataConnectionString)).InstancePerLifetimeScope();
            }
            else
            {
                builder.Register<IDatabase>(context => new CommerceDbContext(dbSettingsManager.LoadSettings().DataConnectionString)).InstancePerLifetimeScope();
            }

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //host.EnableDiscovery();
            var container = builder.Build();

            // make AutofacHostFactory.Container equal to EngineContext.Current
            // after it, should not set new container to AutofacHostFactory.Container or EngineContext.Current
            AutofacHostFactory.Container = container;
            EngineContext.Initialize(new AutofacContainerManager(container), new ContainerConfig(), false);
        }
    }
}