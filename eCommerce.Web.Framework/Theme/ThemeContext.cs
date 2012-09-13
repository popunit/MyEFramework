using eCommerce.Core.Common;
using eCommerce.Core.Data;
using eCommerce.Core.Enums;
using eCommerce.Services;
using eCommerce.Services.Common;
using eCommerce.Services.Users;
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
        private readonly IThemeProvider themeProvider;

        private string desktopThemeName;
        private string mobileThemeName;

        public ThemeContext(
            WorkContextServiceBase workContext,
            IGenericCharacteristicDataService genericCharacteristicService,
            StoreStateSettings storeStateSettings,
            IThemeProvider themeProvider
            )
        {
            this.workContext = workContext;
            this.genericCharacteristicService = genericCharacteristicService;
            this.storeStateSettings = storeStateSettings;
            this.themeProvider = themeProvider;

            // initial theme name using null, represent there is no theme name cached.
            desktopThemeName = null;
            mobileThemeName = null;
        }

        public string GetCurrentTheme(WorkType type)
        {
            switch (type)
            {
                case WorkType.Mobile:
                    {
                        throw new NotImplementedException();
                    }
                case WorkType.Desktop:
                default:
                    {
                        if (!desktopThemeName.IsNull())
                            return desktopThemeName;
                        var themeName = workContext.CurrentUser.GetWorkingDesktopThemeName(UserCharacteristicResource.DesktopThemeName);
                        if (!themeProvider.ThemeConfigExists(themeName)) // if theme configuration doesn't exist, search all the desktop themes and select one among them
                            themeName = themeProvider.GetThemeConfigurations()
                                .Where(x => !x.IsForMobile).FirstOrDefault().ThemeName;

                        desktopThemeName = themeName;
                        return themeName;
                    }
            }
        }

        public bool SetTheme(WorkType type)
        {
            throw new NotImplementedException();
        }
    }
}
