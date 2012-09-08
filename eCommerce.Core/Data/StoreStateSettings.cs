using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Configuration;
using eCommerce.Core.Enums;

namespace eCommerce.Core.Data
{
    /// <summary>
    /// Store (database, cookie etc) state settings
    /// </summary>
    public class StoreStateSettings : ISettings
    {
        public string StoreName { get; set; }
        public string StoreUrl { get; set; }
        public bool StoreClosed { get; set; }

        /// <summary>
        /// Default desktop theme
        /// </summary>
        public string DefaultStoredThemeForDesktop { get; set; }
        public bool MobileDevicesSupported { get; set; }

        // for test
        public bool EmulateMobileDevice { get; set; }

        /// <summary>
        /// MiniProfile is an excellent 3rd-party lib, set the flag to enable MiniProfile in ASP.MVC views
        /// </summary>
        public bool EnableMiniProfile { get; set; }
    }
}
