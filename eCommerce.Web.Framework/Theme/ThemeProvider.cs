using eCommerce.Core;
using eCommerce.Core.Common;
using eCommerce.Core.Common.Web;
using eCommerce.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace eCommerce.Web.Framework.Theme
{
    /// <summary>
    /// Theme provider for getting proper theme content
    /// </summary>
    /// <remarks>theme config file is associated with base config file</remarks>
    public class ThemeProvider : IThemeProvider
    {
        private string themeConfigPath = string.Empty;
        private readonly IList<ThemeConfig> themeConfigurations;

        public ThemeProvider(Config config)
        {
            this.themeConfigurations = new List<ThemeConfig>(); // set empty
            this.themeConfigPath = WebsiteHelper.MapPath(config.Themes.BasePath); // if Themes.BasePath is not set, will return empty
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath">base path recorded in base config</param>
        protected virtual void LoadConfiguration(string basePath)
        {
            // search configuration files
            foreach (string themePath in Directory.GetDirectories(basePath))
            {
                var config = LoadThemeConfig(themePath);
                if (null != config) // find the config
                    themeConfigurations.Add(config);
            }
        }

        private ThemeConfig LoadThemeConfig(string themePath)
        {
            var themeDirectory = new DirectoryInfo(themePath); // do it for avoiding handling '/' at the end
            var themeConfigFile = new FileInfo(Path.Combine(themeDirectory.FullName, "theme.config"));

            if (themeConfigFile.Exists)
            {
                var config = new XmlDocument();
                config.Load(themeConfigFile.FullName);
                return new ThemeConfig(themeDirectory.Name, themeDirectory.FullName, config);
            }

            return null;
        }

        public ThemeConfig GetThemeConfiguration(string themeName)
        {
            return themeConfigurations.GetItem(i =>
                i.ThemeName.Equals(themeName, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<ThemeConfig> GetThemeConfigurations()
        {
            return themeConfigurations;
        }
    }
}
