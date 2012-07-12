using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using eCommerce.Core.Enums;

namespace eCommerce.Web.Framework.Mvc
{
    internal class MvcHelper
    {
        internal static string GetCurrentTheme(WorkType workType)
        {
            var themeContext = DependencyResolver.Current.GetService<IThemeContext>();
            return themeContext.GetCurrentTheme(workType);
        }
    }
}
