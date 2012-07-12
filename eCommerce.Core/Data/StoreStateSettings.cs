using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Configuration;
using eCommerce.Core.Enums;

namespace eCommerce.Core.Data
{
    public class StoreStateSettings : ISettings
    {
        public string StoreName { get; set; }
        public string StoreUrl { get; set; }
        public bool StoreClosed { get; set; }
        public Dictionary<WorkType, string> DefaultStoreTheme { get; set; }
        public bool MobileDevicesSupported { get; set; }

        // for test
        public bool EmulateMobileDevice { get; set; }

        /// <summary>
        /// MiniProfile is an excellent 3rd-party lib, set the flag to enable MiniProfile in ASP.MVC views
        /// </summary>
        public bool EnableMiniProfile { get; set; }
    }
}
