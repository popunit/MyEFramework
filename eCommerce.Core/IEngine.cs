using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Configuration;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.JobService;

namespace eCommerce.Core
{
    /// <summary>
    /// Core Engine interface to build infrastructure.
    /// </summary>
    /// <remarks>focus on container manager and corresponding configuration</remarks>
    public interface IEngine
    {
        IContainerManager ContainerManager { get; }
        T Resolve<T>(string key) where T : class;
        T[] ResolveAll<T>() where T : class;
        void Init(
            IContainerManager containerManager, 
            EventBroker broker, 
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
        protected IContainerManager containerManager;
        protected IContainerConfig containerConfig;

        public IContainerManager ContainerManager
        {
            get { return this.containerManager; }
        }

        public IContainerConfig ContainerConfiguration
        {
            get { return this.containerConfig; }
        }

        public T Resolve<T>(string key) where T : class
        {
            return containerManager.Resolve<T>(key);
        }

        public T[] ResolveAll<T>() where T : class
        {
            return containerManager.ResolveAll<T>();
        }

        public void Init(IContainerManager containerManager, IContainerConfig containerConfig)
        {
            Init(containerManager, EventBroker.Current, containerConfig);
        }

        public void Init(IContainerManager containerManager, EventBroker broker, IContainerConfig containerConfig)
        {
            var config = ConfigHelper.ReadonlySection;

            Init(containerManager, broker, containerConfig, config);
            
        }

        public void Init(IContainerManager containerManager, EventBroker broker, IContainerConfig containerConfig, Config config)
        {
            this.containerManager = containerManager;
            this.containerConfig = containerConfig;
            this.containerConfig.Init(this, containerManager, broker, config);

            if (DatabaseSettingHelper.FindDatabaseSettings)
            {
                RunTasks();
            }
            else
            {
                // TO-DO
            }
        }

        protected virtual void RunTasks()
        {
            var routing = containerManager.Resolve<IRoute>(typeof(WebsiteRoute).Name);
            RouteHelper.RoutingToExecute<ITask>(routing, i => i.Execute());
        }

        public JobHandler JobService
        {
            get { throw new NotImplementedException(); }
        }
    }
}
