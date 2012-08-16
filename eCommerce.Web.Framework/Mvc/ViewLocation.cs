using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;

namespace eCommerce.Web.Framework.Mvc
{
    /// <summary>
    /// Represent view location, including location format and format arguments
    /// </summary>
    public class ViewLocation
    {
        private readonly string virtualPathFormatString;
        public ViewLocation(string virtualPathFormatString)
        {
            this.virtualPathFormatString = virtualPathFormatString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="controllerName"></param>
        /// <param name="areaName"></param>
        /// <param name="theme"></param>
        /// <returns></returns>
        /// <remarks>Parameters' order must be fixed</remarks>
        public virtual string Format(string viewName, string controllerName, string areaName, string theme)
        {
            return string.Format(CultureInfo.InvariantCulture, virtualPathFormatString, viewName, controllerName, theme);
        }
    }
}
