using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{
    public interface IPlugin
    {
        IPluginDescriptor Descriptor { get; set; }

        bool Install();

        bool Uninstall();
    }
}
