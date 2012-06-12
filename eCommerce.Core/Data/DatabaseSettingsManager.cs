using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using eCommerce.Core.Configuration;

namespace eCommerce.Core.Data
{
    public class DatabaseSettingsManager
    {
        protected readonly string Separator = ConfigHelper.ReadonlySection.DatabaseSetting.Separator;
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
 
            }

            throw new NotImplementedException();
        }
    }
}
