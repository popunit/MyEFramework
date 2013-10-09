using eCommerce.Core.Configuration;

namespace eCommerce.Core.Data
{
    /// <summary>
    /// Store (database, cookie, theme etc) state settings and other settings for entire website
    /// </summary>
    /// <remarks>Global Settings (impact all the users)</remarks>
    public class StoreStateSettings : ISettings
    {
        private string _defaultStoredThemeForMobile;

        public string StoreName { get; set; }
        public string StoreUrl { get; set; }
        public bool StoreClosed { get; set; }

        /// <summary>
        /// Default desktop theme
        /// </summary>
        public string DefaultStoredThemeForDesktop { get; set; }
        
        /// <summary>
        /// Indicate if mobile device is supported
        /// </summary>
        /// <remarks>
        /// if false, all the settings referred to mobile cannot work
        /// </remarks>
        public bool MobileDevicesSupported { get; set; }

        /// <summary>
        /// Default mobile theme (if supported)
        /// </summary>
        public string DefaultStoredThemeForMobile
        {
            get 
            {
                if (MobileDevicesSupported)
                    return _defaultStoredThemeForMobile;
                else
                    return null; // if mobile devices cannot be supported, always return null
            }
            set 
            {
                this._defaultStoredThemeForMobile = value;
            }
        }

        /// <summary>
        /// Whether the user can be allowed to select theme
        /// </summary>
        /// <remarks>
        /// This property only work for desktop mode, because mobile mode cannot support theme 
        /// select so far.
        /// </remarks>
        public bool SelectThemeByUsersIsAllowed { get; set; }

        // for test
        public bool EmulateMobileDevice { get; set; }

        /// <summary>
        /// MiniProfile is an excellent 3rd-party lib, set the flag to enable MiniProfile in ASP.MVC views
        /// </summary>
        public bool EnableMiniProfile { get; set; }
    }
}
