using eCommerce.Core.Configuration;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.JobService;
using System;

namespace eCommerce.Core
{
    /// <summary>
    /// Core Engine interface to build infrastructure.
    /// </summary>
    /// <remarks>focus on container manager and corresponding configuration</remarks>
    public interface IEngine
    {
        IContainerManager ContainerManager { get; }
        T Resolve<T>(string key = "") where T : class;
        object Resolve(Type type);
        T[] ResolveAll<T>() where T : class;
        void Init(
            IContainerManager containerManager,
            //EventBroker broker,
            IContainerConfig containerConfig,
            Config config);
        JobHandler JobService { get; }
    }

    /// <summary>
    /// Core Engine
    /// </summary>
    /// <remarks>[Component]</remarks>
    public abstract class EngineBase : IEngine
    {
        private IContainerManager _containerManager;
        private IContainerConfig _containerConfig;

        public IContainerManager ContainerManager
        {
            get { return this._containerManager; }
        }

        public IContainerConfig ContainerConfiguration
        {
            get { return this._containerConfig; }
        }

        public T Resolve<T>(string key = "") where T : class
        {
            return _containerManager.Resolve<T>(key);
        }

        public T[] ResolveAll<T>() where T : class
        {
            return _containerManager.ResolveAll<T>();
        }

        //public void Init(IContainerManager containerManager, IContainerConfig containerConfig)
        //{
        //    Init(containerManager, EventBroker.Current, containerConfig);
        //}

        public void Init(IContainerManager containerManager, IContainerConfig containerConfig)
        {
            var config = ConfigHelper.ReadonlySection;

            Init(containerManager, containerConfig, config);
            
        }

        public void Init(IContainerManager containerManager, IContainerConfig containerConfig, Config config)
        {
            this._containerManager = containerManager;
            this._containerConfig = containerConfig;
            this._containerConfig.Init(this, containerManager, EventBroker.Current, config);

            // only true in database server, in web site, cannot find the database specified
            if (DatabaseSettingHelper.FindDatabaseSettings)
            {
                RunTasks();
            }
            else
            {
                RunWarmupTasks();
            }
        }

        protected virtual void RunTasks()
        {
            var searcher = _containerManager.Resolve<ISearcher>(typeof(WebsiteSearcher).Name);
            searcher.RoutingToExecute<ITask>(i => i.Execute());
        }

        protected virtual void RunWarmupTasks()
        {
            var searcher = _containerManager.Resolve<ISearcher>(typeof(WebsiteSearcher).Name);
            searcher.RoutingToExecute<IWarmupTask>(i => i.Execute());
        }

        public JobHandler JobService
        {
            get { throw new NotImplementedException(); }
        }


        public object Resolve(Type type)
        {
            return _containerManager.Resolve(type);
        }
    }
}
