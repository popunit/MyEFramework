using eCommerce.Core;
using eCommerce.Core.Common.Web;
using eCommerce.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Framework.Theme
{
    /// <summary>
    /// Theme provider for getting proper theme content
    /// </summary>
    /// <remarks>theme config file is associated with base config file</remarks>
    public class ThemeProvider : IThemeProvider
    {
        private string themeConfigPath = string.Empty;
        private readonly IEnumerable<ThemeConfig> themeConfigurations;

        public ThemeProvider(Config config, IHttpHelper httpHelper)
        {
            this.themeConfigurations = new List<ThemeConfig>(); // set empty
            this.themeConfigPath = WebsiteHelper.MapPath(config.Themes.BasePath); // if Themes.BasePath is not set, will return empty
        }

        public ThemeConfig GetThemeConfiguration(string themeName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ThemeConfig> GetThemeConfigurations()
        {
            throw new NotImplementedException();
        }
    }
}
