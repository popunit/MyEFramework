using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Configuration
{
    /// <summary>
    /// Setting for page display
    /// </summary>
    /// <remarks>should config it by configuration provider</remarks>
    public class PageSettings : ISettings
    {
        public string DefaultTitle { get; set; }
    }
}
