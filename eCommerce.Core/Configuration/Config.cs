using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Configuration
{
    public enum Trust
    {
        FullTrust,
        Medium
    }

    public class Config : ConfigurationSection
    {
        [ConfigurationProperty("automation")]
        public AutomationElement Automation
        {
            get
            {
                return (AutomationElement)this["automation"];
            }
            set 
            {
                this["automation"] = value;
            }
        }

        [ConfigurationProperty("engine")]
        public EngineElement Engine
        {
            get
            {
                return (EngineElement)this["engine"];
            }
            set
            {
                this["engine"] = value;
            }
        }

        [ConfigurationProperty("databasesetting")]
        public DatabaseSettingElement DatabaseSetting
        {
            get
            {
                return (DatabaseSettingElement)this["databasesetting"];
            }
            set
            {
                this["databasesetting"] = value;
            }
        }

        [ConfigurationProperty("themes")]
        public ThemesElement Themes
        {
            get
            {
                return (ThemesElement)this["themes"];
            }
            set
            {
                this["themes"] = value;
            }
        }
    }

    #region Configuration Element

    public class AutomationElement : ConfigurationElement
    {
        [ConfigurationProperty("enabled", DefaultValue = false, IsRequired = false)]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
            set
            {
                this["enable"] = value;
            }
        }
    }

    public class EngineElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = false)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }
    }

    public class DatabaseSettingElement : ConfigurationElement
    {
        [ConfigurationProperty("associatedfile", IsRequired = false, DefaultValue = "~/dbsettings.txt")]
        public string AssociatedFile
        {
            get
            {
                return (string)this["associatedfile"];
            }
            set
            {
                this["associatedfile"] = value;
            }
        }

        [ConfigurationProperty("separator", IsRequired = false, DefaultValue = ':')]
        public char Separator
        {
            get
            {
                return (char)this["separator"];
            }
            set
            {
                this["separator"] = value;
            }
        }

        [ConfigurationProperty("isfaked", IsRequired = false, DefaultValue = false)]
        public bool IsFaked
        {
            get
            {
                return (bool)this["isfaked"];
            }
            set
            {
                this["isfaked"] = value;
            }
        }
    }

    public class ThemesElement : ConfigurationElement
    {
        [ConfigurationProperty("basePath", IsRequired = false)]
        public string BasePath
        {
            get
            {
                return (string)this["basePath"];
            }
            set
            {
                this["basePath"] = value;
            }
        }
    }

    #endregion
}
