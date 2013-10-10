using eCommerce.Core.Enums;
using System.Web;
using System.Web.Mvc;

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
