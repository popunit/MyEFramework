using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Configuration
{
    /// <summary>
    /// Used to register configs and engine
    /// </summary>
    /// <remarks>[Component]</remarks>
    public class ContainerConfig : ContainerConfigBase
    {
        public override void Init(IEngine engine, IContainerManager containerManager, Infrastructure.EventBroker broker, Config configuration)
        {
            base.Init(engine, containerManager, broker, configuration);
        }
    }
}
