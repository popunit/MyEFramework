using System;
using System.Net;
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

        public static string IPv4Address(this HttpContextBase httpContext)
        {
            if (httpContext.IsNull() || httpContext.Request.IsNull())
                return string.Empty;
            string ip4Address = String.Empty;

            foreach (IPAddress ipa in Dns.GetHostAddresses(httpContext.Request.UserHostAddress))
            {
                if (ipa.AddressFamily.ToString() == "InterNetwork")
                {
                    ip4Address = ipa.ToString();
                    break;
                }
            }

            return ip4Address;
        }

        public static string CurrentMachineIPv4()
        {
            string ip4Address = string.Empty;
            foreach (IPAddress ipa in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ipa.AddressFamily.ToString() == "InterNetwork")
                {
                    ip4Address = ipa.ToString();
                    break;
                }
            }

            return ip4Address;
        }
    }
}
