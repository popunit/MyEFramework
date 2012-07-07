using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eCommerce.Core
{
    public interface IMobileDeviceCheck
    {
        bool IsMobileDevice(HttpContextBase httpContext);
        bool IsMobileDeviceSupported();
    }
}
