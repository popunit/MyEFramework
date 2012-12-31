using Autofac;
using eCommerce.Core;
using eCommerce.Core.Data;
using eCommerce.Data.DataProvider;
using eCommerce.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.Injection
{
    public class DatabaseRegistrar : RegistrarBase<ContainerBuilder>
    {
        public override void Register(ContainerBuilder builder, Core.Infrastructure.ISearcher route)
        {
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

            if (!dbSettingsManager.IsFaked)
            {
                builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterGeneric(typeof(FaskeRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            }
        }

        public override int Order
        {
            get { return 0; }
        }
    }
}
