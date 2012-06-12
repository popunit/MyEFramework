using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Configuration;
using eCommerce.Core.Infrastructure;

namespace eCommerce.Core
{
    /// <summary>
    /// Core Engine interface to build infrastructure.
    /// </summary>
    /// <remarks>focus on container manager and corresponding configuration</remarks>
    public interface IEngine
    {
        IContainerManager Manager { get; }
        T Resolve<T>() where T : class;
        T[] ResolveAll<T>() where T : class;
        void Init(
            IContainerManager containerManager, 
            EventBroker broker, 
            IContainerConfig containerConfig, 
            Config config);
    }

    /// <summary>
    /// Core Engine
    /// </summary>
    /// <remarks>[Component]</remarks>
    public abstract class EngineBase : IEngine
    {
        protected IContainerManager manager;
        protected IContainerConfig containerConfig;

        public IContainerManager Manager
        {
            get { return this.manager; }
        }

        public IContainerConfig ContainerConfiguration
        {
            get { return this.containerConfig; }
        }

        public T Resolve<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T[] ResolveAll<T>() where T : class
        {
            throw new NotImplementedException();
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
            this.manager = containerManager;
            this.containerConfig = containerConfig;
            this.containerConfig.Init(this, containerManager, broker, config);

            //TO-DO
        }

        protected virtual void Run()
        {
        }
    }
}
