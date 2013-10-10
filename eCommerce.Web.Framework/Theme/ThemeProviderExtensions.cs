using System;
using System.Linq;

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
