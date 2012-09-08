using eCommerce.Web.Framework.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
