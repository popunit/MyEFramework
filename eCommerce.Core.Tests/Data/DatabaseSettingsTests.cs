using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;
using eCommerce.Core.Data;
using NUnit.Framework;

namespace eCommerce.Core.Tests.Data
{
    [TestFixture]
    public class DatabaseSettingsTests
    {
        [Test]
        public void Change_Database_Setting_Properties()
        {
            string info1 = "DataProvider changed";
            string info2 = "DataConnectionString changed";
            string result = String.Empty;
            DatabaseSettings settings = new DatabaseSettings();
            settings.PropertyChanged += (s, e) => 
            {
                if (e.PropertyName == 
                    ReflectionUtility.GetPropertyName(() => ((DatabaseSettings)s).DataProvider))
                {
                    result = info1;
                }
                else if(e.PropertyName == 
                    ReflectionUtility.GetPropertyName(() => ((DatabaseSettings)s).DataConnectionString))
                {
                    result = info2;
                }

            };

            settings.DataProvider = "Set Provider";
            Console.Out.WriteLine(settings.DataProvider);
            Assert.AreEqual(info1, result);
            
            settings.DataConnectionString = "Set Connection String";
            Console.Out.WriteLine(settings.DataConnectionString);
            Assert.AreEqual(info2, result);
        }
    }
}
