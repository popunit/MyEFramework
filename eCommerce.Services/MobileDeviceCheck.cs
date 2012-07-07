using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eCommerce.Core;

namespace eCommerce.Services
{
    public class MobileDeviceCheck : IMobileDeviceCheck
    {
        public bool IsMobileDevice(HttpContextBase httpContext)
        {
            throw new NotImplementedException();
        }

        public bool IsMobileDeviceSupported()
        {
            throw new NotImplementedException();
        }
    }
}
