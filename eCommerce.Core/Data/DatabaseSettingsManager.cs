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
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Core.Common.Web;

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

        /// <summary>
        /// virtual path
        /// </summary>
        protected readonly string FilePath = ConfigHelper.ReadonlySection.DatabaseSetting.AssociatedFile;

        protected virtual string MapToPhysicalPath(string virtualPath)
        {
            return AspectF.Define.MustBeNonNull(virtualPath).
                HandleException<ArgumentException>(
                ex => { throw new CommonException(string.Format("The file {0} is not existed!", virtualPath), ex); }).
                Return<string>(() =>
            {
                return WebsiteHelper.MapPath(virtualPath);
            });
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

        protected virtual string ToFormatedString(DatabaseSettings settings)
        {
            if (settings == null)
                return "";

            return string.Format("DataProvider: {0}{2}DataConnectionString: {1}{2}",
                                 settings.DataProvider,
                                 settings.DataConnectionString,
                                 Environment.NewLine
                );
        }

        public virtual DatabaseSettings LoadSettings()
        {
            string filePath = MapToPhysicalPath(FilePath);
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                return ParseSettings(text);
            }
            else
                return new DatabaseSettings();
        }

        public virtual void SaveSettings(DatabaseSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            string filePath = MapToPhysicalPath(FilePath);
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                {
                    //we use 'using' to close the file after it's created
                }
            }

            var text = ToFormatedString(settings);
            File.WriteAllText(filePath, text);
        }
    }
}
