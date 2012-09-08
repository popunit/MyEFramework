using eCommerce.Core.Data;
using eCommerce.Core.Enums;
using eCommerce.Services;
using eCommerce.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Framework.Theme
{
    /// <summary>
    /// Represent current theme in process
    /// </summary>
    /// <remarks>Consider moving it to work context</remarks>
    public class ThemeContext : IThemeContext
    {
        private readonly WorkContextServiceBase workContext;
        private readonly IGenericCharacteristicDataService genericCharacteristicService;
        private readonly StoreStateSettings storeStateSettings;

        public ThemeContext(
            WorkContextServiceBase workContext,
            IGenericCharacteristicDataService genericCharacteristicService,
            StoreStateSettings storeStateSettings
            )
        {
            this.workContext = workContext;
            this.genericCharacteristicService = genericCharacteristicService;
            this.storeStateSettings = storeStateSettings;
        }

        public string GetCurrentTheme(WorkType type)
        {
            throw new NotImplementedException();
        }

        public bool SetTheme(WorkType type)
        {
            throw new NotImplementedException();
        }
    }
}
