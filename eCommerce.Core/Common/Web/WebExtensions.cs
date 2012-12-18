using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eCommerce.Core.Common.Web
{
    public static class WebExtensions
    {
        public static bool IsValid(this HttpCookie cookie)
        {
            if (!cookie.IsNull() && !string.IsNullOrEmpty(cookie.Value))
                return true;
            else
                return false;
        }
    }
}
