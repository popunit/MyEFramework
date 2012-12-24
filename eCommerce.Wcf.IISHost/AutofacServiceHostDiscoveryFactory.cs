using Autofac.Integration.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using eCommerce.Core.Caching;
using eCommerce.Core;
using eCommerce.Wcf.Services.Users;
using eCommerce.Wcf.Services.Common;
using eCommerce.Core.Configuration;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Data;
using eCommerce.Data.DataProvider;
using eCommerce.Data;
using eCommerce.Data.Repositories;
using Autofac;
using System.IO;
using System.Web.Routing;
using System.ServiceModel.Activation;

namespace eCommerce.Wcf.IISHost
{
    public class AutofacServiceHostDiscoveryFactory : AutofacServiceHostFactory
    {
        /// <summary>
        /// Initial container at the beginning of server activation
        /// </summary>
        /// <remarks>http://www.eidias.com/Blog/2012/2/13/simple-wcf-hosting-wcf-service-by-autofac-in-aspnet-mvc-3</remarks>
        static AutofacServiceHostDiscoveryFactory()
        {            
            var builder = new ContainerBuilder();

            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("static_cache_manager").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("per_request_cache_manager").InstancePerLifetimeScope();

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

            //host.EnableDiscovery();
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);
            return host;
        }
    }
}