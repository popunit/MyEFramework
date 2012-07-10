using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eCommerce.Core;
using eCommerce.Core.Data;
using eCommerce.Services.Users;

namespace eCommerce.Services
{
    public class MobileDeviceCheck : IMobileDeviceCheck
    {
        private readonly StoreStateSettings storeStateSettings;
        private readonly WorkContextServiceBase workContext;

        public MobileDeviceCheck(StoreStateSettings settings, WorkContextServiceBase context)
        {
            this.storeStateSettings = settings;
            this.workContext = context;
        }

        public bool IsMobileDevice(HttpContextBase httpContext)
        {
            // check API
            return httpContext.Request.Browser.IsMobileDevice ||
                storeStateSettings.EmulateMobileDevice;
        }

        public bool IsMobileDeviceSupported()
        {
            return storeStateSettings.MobileDevicesSupported;
        }

        public bool MobileDeviceSupportedIsClosed()
        {
            return workContext.CurrentUser.GetCharacteristic<bool>(CharacteristicResource.MobileDeviceSupportedIsClosed);
        }
    }
}
