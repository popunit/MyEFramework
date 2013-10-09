using System;

namespace eCommerce.Core.Data
{
    public class DatabaseSettingHelper
    {
        private static bool? _findDatabaseSettings;
        public static bool FindDatabaseSettings
        {
            get
            {
                if (!_findDatabaseSettings.HasValue)
                {
                    var datasettingsManager = new DatabaseSettingsManager();
                    var settings = datasettingsManager.LoadSettings();
                    _findDatabaseSettings =
                        settings != null && !String.IsNullOrEmpty(settings.DataConnectionString);
                }
                return _findDatabaseSettings.Value;
            }
        }
    }
}
