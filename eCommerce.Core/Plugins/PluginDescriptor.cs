﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Plugins
{
    /// <summary>
    /// Descriptor for plugin, it is comparable for version update.
    /// </summary>
    public class PluginDescriptor : IComparable<IPluginDescriptor>, IPluginDescriptor
    {
        public int CompareTo(IPluginDescriptor other)
        {
            throw new NotImplementedException();
        }
    }
}
