﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Configuration;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Common;
using eCommerce.Core.Data;

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

            // register type
            containerManager.AddComponent<IRoute, WebsiteRoute>(typeof(WebsiteRoute).Name);
            // TO-DO: Check if I register this time multi times (should not)
            containerManager.AddComponent<DatabaseSettings>(typeof(DatabaseSettings).Name);
            // TO-DO: Check if register it before and figure out how to set it (should be registered in IRegistrar)
            containerManager.AddComponentInstance<StoreStateSettings>(new StoreStateSettings { EnableMiniProfile = true }, typeof(StoreStateSettings).Name);
           
            // register IRegistrar
            containerManager.UpdateContainer(build =>
            {
                var routing = containerManager.Resolve<IRoute>(typeof(WebsiteRoute).Name);
                RouteHelper.RoutingToExecute<IRegistrar>(routing, i => i.Register(build, routing));
            });

            // TO-DO
        }

        
    }
}
