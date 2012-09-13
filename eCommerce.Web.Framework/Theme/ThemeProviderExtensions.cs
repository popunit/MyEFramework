using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;

namespace eCommerce.Web.Framework.Theme
{
    public static class ThemeProviderExtensions
    {
        public static bool ThemeConfigExists(this IThemeProvider themeProvider, string themeName)
        {
            return themeProvider.GetThemeConfigurations().
                Any(configuration => configuration.ThemeName.Equals(themeName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
