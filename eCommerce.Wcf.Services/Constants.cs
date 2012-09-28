using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Wcf.Services
{
    internal static class Constants
    {
        #region Cache Key Patterns for Entities

        internal const string CACHE_GENERICCHARACTERISTIC_FORMAT = "SERVICE.DATA.GENERICCHARACTERISTIC.{0}_{1}";

        internal const string CACHE_GENERICCHARACTERISTIC_PATTERN = "SERVICE.DATA.GENERICCHARACTERISTIC";
        internal const string CACHE_USERROLE_PATTERN = "SERVICE.DATA.USERROLE";

        #endregion
    }
}
