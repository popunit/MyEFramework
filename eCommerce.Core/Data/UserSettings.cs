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
    /// </summary>
    /// <remarks>Move to WebWorkContext?</remarks>
    public class UserSettings : ISettings
    {
        public bool StoreLastVisitedPage { get; set; }
    }
}
