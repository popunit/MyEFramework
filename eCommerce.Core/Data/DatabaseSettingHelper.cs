using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Data
{
    public class DatabaseSettingHelper
    {
        private static bool? findDatabaseSettings;
        public static bool FindDatabaseSettings
        {
            get
            {
                if (!findDatabaseSettings.HasValue)
                {
                    var datasettingsManager = new DatabaseSettingsManager();
                    var settings = datasettingsManager.LoadSettings();
                    findDatabaseSettings =
                        settings != null && !String.IsNullOrEmpty(settings.DataConnectionString);
                }
                return findDatabaseSettings.Value;
            }
        }
    }
}
