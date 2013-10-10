using eCommerce.Web.Framework.Theme;
using System.Collections.Generic;

namespace eCommerce.Web.Framework
{
    /// <summary>
    /// For getting proper theme configuration
    /// </summary>
    public interface IThemeProvider
    {
        ThemeConfig GetThemeConfiguration(string themeName);
        IEnumerable<ThemeConfig> GetThemeConfigurations();
    }
}
