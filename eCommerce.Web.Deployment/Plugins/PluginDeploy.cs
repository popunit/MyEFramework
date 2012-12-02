using eCommerce.Core;
using eCommerce.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Deployment.Plugins
{
    public class PluginDeploy : ISingleton
    {
        public void RegisterAsSingleton()
        {
            Singleton<PluginDeploy>.Instance = this;
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
