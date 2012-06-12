using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Configuration
{
    public static class ConfigHelper
    {
        public static class Constants
        {
            public const string SectionName = "core";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Can be changed anytime
        /// </remarks>
        public static Config Section
        {
            get 
            {
                return ConfigurationManager.GetSection(Constants.SectionName) as Config;
            }
        }

        private static readonly Config rdSection = 
            ConfigurationManager.GetSection(Constants.SectionName) as Config;

        /// <summary>
        /// Only return the initialized results
        /// </summary>
        public static Config ReadonlySection
        {
            get
            {
                return rdSection;
            }
        }
    }
}
