using System.Globalization;

namespace eCommerce.Web.Framework.Mvc
{
    /// <summary>
    /// Represent view location, including location format and format arguments
    /// </summary>
    public class ViewLocation
    {
        protected readonly string VirtualPathFormatString;
        public ViewLocation(string virtualPathFormatString)
        {
            this.VirtualPathFormatString = virtualPathFormatString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="controllerName"></param>
        /// <param name="areaName">this parameter is useless for view location</param>
        /// <param name="theme"></param>
        /// <returns></returns>
        /// <remarks>Parameters' order must be fixed</remarks>
        public virtual string Format(string viewName, string controllerName, string areaName, string theme)
        {
            return string.Format(CultureInfo.InvariantCulture, VirtualPathFormatString, viewName, controllerName, theme);
        }
    }

    /// <summary>
    /// Represent area view location, including location format and format arguments
    /// </summary>
    public class AreaAwareViewLocation : ViewLocation
    {
        public AreaAwareViewLocation(string virtualPathFormatString)
            : base(virtualPathFormatString)
        { 
        }

        public override string Format(string viewName, string controllerName, string areaName, string theme)
        {
            return string.Format(CultureInfo.InvariantCulture, VirtualPathFormatString, viewName, controllerName, areaName, theme);
        }
    }
}
