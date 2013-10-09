using System.Web;

namespace eCommerce.Core
{
    public interface IMobileDeviceCheck
    {
        bool IsMobileDevice(HttpContextBase httpContext);
        bool IsMobileDeviceSupported();
        bool MobileDeviceSupportedIsClosed();
        bool MobileDeviceIsAvailable(HttpContextBase httpContext);
    }
}
