using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Configuration;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Common;

namespace eCommerce.Core
{
    public interface IContainerConfig
    {
        void Init(IEngine engine, IContainerManager containerManager, EventBroker broker, Config configuration);
    }

    public abstract class ContainerConfigBase : IContainerConfig
    {
        /// <summary>
        /// register necessary type
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="containerManager"></param>
        /// <param name="broker"></param>
        /// <param name="configuration"></param>
        public virtual void Init(IEngine engine, IContainerManager containerManager, EventBroker broker, Config configuration)
        {
            // register singleton instance
            containerManager.AddComponentInstance<Config>(configuration, configuration.GetType().Name);
            containerManager.AddComponentInstance<IEngine>(engine, engine.GetType().Name);
            containerManager.AddComponentInstance<EventBroker>(broker, broker.GetType().Name);
            containerManager.AddComponentInstance<ContainerConfigBase>(this, this.GetType().Name);
            containerManager.AddComponent<IRoute, WebsiteRoute>(typeof(WebsiteRoute).Name);
           
            containerManager.UpdateContainer(build =>
            {
                var routing = containerManager.Resolve<IRoute>(typeof(WebsiteRoute).Name);
                var types = routing.FindType<IRegistrar>();
                var instances = new List<IRegistrar>();
                types.ForEach(t => 
                {
                    instances.Add((IRegistrar)Activator.CreateInstance(t));
                });

                // register object in order
                instances.OrderBy(t => t.Order).ForEach(i => i.Register(build, routing));
            });

            // TO-DO
        }
    }
}
