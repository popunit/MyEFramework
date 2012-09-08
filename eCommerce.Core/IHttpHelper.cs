using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{
    /// <summary>
    /// Helper for HTTP info
    /// </summary>
    public interface IHttpHelper
    {
        /// <summary>
        /// Get last visited page url from current client (go back)
        /// </summary>
        /// <returns></returns>
        string GetUrlReferrer();

        /// <summary>
        /// Get request's url
        /// </summary>
        /// <param name="fullUrl">true: the url will include query strings if have</param>
        /// <returns></returns>
        string GetCurrentRequestUrl(bool fullUrl);
    }
}
