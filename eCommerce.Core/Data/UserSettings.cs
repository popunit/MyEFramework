using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Configuration;

namespace eCommerce.Core.Data
{
    /// <summary>
    /// Settings for specific user
    /// relationship with UserCharacteristic
    /// </summary>
    /// <remarks>Move to WebWorkContext?</remarks>
    public class UserSettings : ISettings
    {
        /// <summary>
        /// if true, the visited url will be stored in UserCharacteristic table
        /// </summary>
        public bool StoreLastVisitedPage { get; set; }

        /// <summary>
        /// if true, use user email, if false, use user name
        /// </summary>
        public bool ProvideUserEmail { get; set; }
    }
}
