using System.Web;

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
        /// <param name="includeQueryString">true: the url will include query strings if have</param>
        /// <returns></returns>
        string GetCurrentRequestUrl(bool includeQueryString);

        /// <summary>
        /// Get cookie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        HttpCookie GetCookie(string name);

        /// <summary>
        /// Set cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void SetCookie(string name, string value);
    }
}
