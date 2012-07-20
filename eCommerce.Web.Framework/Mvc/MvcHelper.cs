using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using eCommerce.Core.Enums;

namespace eCommerce.Web.Framework.Mvc
{
    internal static class MvcHelper
    {
        internal static string GetCurrentTheme(WorkType workType)
        {
            var themeContext = DependencyResolver.Current.GetService<IThemeContext>();
            return themeContext.GetCurrentTheme(workType);
        }

        internal static bool HasRequest(this HttpContextBase httpContext)
        {
            if (null != httpContext)
                if (null != httpContext.Request)
                    return true;

            return false;
        }
    }
}
