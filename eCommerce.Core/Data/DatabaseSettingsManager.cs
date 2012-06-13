using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using eCommerce.Core.Configuration;
using eCommerce.Exception;
using eCommerce.Core.Common;

namespace eCommerce.Core.Data
{
    public enum DatabaseEnum
    {
        DataProvider,
        DataConnectionString,
        Unknown
    }

    public class DatabaseSettingsManager
    {
        protected readonly char Separator = ConfigHelper.ReadonlySection.DatabaseSetting.Separator;
        protected readonly string FileName = ConfigHelper.ReadonlySection.DatabaseSetting.AssociatedFile;

        protected virtual string MapToPhysicalPath(string virtualPath)
        {
            // check if the website is hosted or not
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(virtualPath);
            }
            else
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory; // get application current running directory
                string relativePath = virtualPath.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, relativePath);
            }
        }

        /// <summary>
        /// Convert setting file to DatabaseSetting object
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual DatabaseSettings ParseSettings(string text)
        {
            DatabaseSettings settings = new DatabaseSettings();
            if (String.IsNullOrEmpty(text))
                return settings;

            var items = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                    items.Add(str);
            }

            foreach (var item in items)
            {
                var keyPair = item.Split(Separator);
                if (keyPair.Count() == 0)
                    continue;
                if (keyPair.Count() != 2)
                    throw new CommonException("Database setting is not correct.");
                string key = keyPair[0];
                string value = keyPair[1];
                DatabaseEnum keyEnum;
                if (!Enum.TryParse(key, true, out keyEnum))
                    keyEnum = DatabaseEnum.Unknown;
                switch (keyEnum) // only get last data provider and data connection string
                {
                    case DatabaseEnum.DataProvider:
                        settings.DataProvider = value;
                        break;
                    case DatabaseEnum.DataConnectionString:
                        settings.DataConnectionString = value;
                        break;
                    default:
                        settings.UnknownItems.Add(key, value);
                        break;
                }
            }

            return settings;
        }
    }
}
