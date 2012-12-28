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
            if (null == httpContext || null == httpContext.Request)
                return string.Empty;
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(httpContext.Request.UserHostAddress))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }

        public static string CurrentMachineIPv4()
        {
            string IP4Address = string.Empty;
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }
    }
}
