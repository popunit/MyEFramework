using eCommerce.Core;
using eCommerce.Core.Data;
using eCommerce.Data.Resources;
using eCommerce.Services.Extensions;
using System.Web;

namespace eCommerce.Services
{
    public class MobileDeviceCheck : IMobileDeviceCheck
    {
        private readonly StoreStateSettings storeStateSettings;
        private readonly WebWorkContextBase workContext;

        public MobileDeviceCheck(StoreStateSettings settings, WebWorkContextBase context)
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
            return workContext.CurrentUser.GetCharacteristic<bool>(UserCharacteristicResource.MobileDeviceSupportedIsClosed);
        }


        public bool MobileDeviceIsAvailable(HttpContextBase httpContext)
        {
            return IsMobileDevice(httpContext) &&
                IsMobileDeviceSupported() &&
                !MobileDeviceSupportedIsClosed();
        }
    }
}
