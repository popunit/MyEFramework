using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;
using eCommerce.Core.Configuration;

namespace eCommerce.Core.Infrastructure
{
    public class EngineContext
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(
            IContainerManager containerManager, IContainerConfig containerConfig, bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                var config = ConfigurationManager.GetSection("core") as Config;
                Singleton<IEngine>.Instance = CreateEngineInstance(config);
                Singleton<IEngine>.Instance.Init(containerManager, containerConfig, config);
            }
            return Singleton<IEngine>.Instance;
        }

        public static IEngine CreateEngineInstance(Config config)
        {
            if (config != null && !string.IsNullOrEmpty(config.Engine.Type))
            {
                var engineType = Type.GetType(config.Engine.Type);
                if (engineType == null)
                    throw new ConfigurationErrorsException("The type '" + engineType + "' could not be found. Please check the configuration at /configuration/nop/engine[@engineType] or check for missing assemblies.");
                //if (!typeof(IEngine).IsAssignableFrom(engineType))
                if(!engineType.IsInherit(typeof(IEngine)))
                    throw new ConfigurationErrorsException("The type '" + engineType + "' doesn't implement 'Nop.Core.Infrastructure.IEngine' and cannot be configured in /configuration/nop/engine[@engineType] for that purpose.");
                return Activator.CreateInstance(engineType) as IEngine;
            }

            return new Engine();
        }

        public static IEngine Current
        {
            get
            {
                return Singleton<IEngine>.Instance;
            }
        }
    }
}
