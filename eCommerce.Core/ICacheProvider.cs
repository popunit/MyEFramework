using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{
    /// <summary>
    /// Work with config to return proper cache manager
    /// </summary>
    public interface ICacheProvider
    {
        ICacheManager Cache { get; }
    }
}
