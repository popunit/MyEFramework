using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Configuration;
using NUnit.Framework;

namespace eCommerce.Core.Tests.Configuration
{
    [TestFixture]
    public class ConfigTest
    {
        [Test]
        public void Get_Data_By_Config()
        {
            Config config = ConfigHelper.Section;
            Assert.AreEqual(config.Automation.Enabled, true);
        }
    }
}
