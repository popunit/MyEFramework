using eCommerce.Core.Common;
using eCommerce.Core.Data;
using eCommerce.Core.Enums;
using eCommerce.Services;
using eCommerce.Services.Common;
using eCommerce.Services.Extensions;
using System.Linq;

namespace eCommerce.Web.Framework.Theme
{
    /// <summary>
    /// Represent current theme in process
    /// </summary>
    /// <remarks>Consider moving it to work context</remarks>
    public class ThemeContext : IThemeContext
    {
        private readonly WebWorkContextBase workContext;
        private readonly IGenericCharacteristicDataService genericCharacteristicService;
        private readonly StoreStateSettings storeStateSettings;
        private readonly IThemeProvider themeProvider;

        private string desktopThemeName;
        private string mobileThemeName;

        public ThemeContext(
            WebWorkContextBase workContext,
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
                        if (!mobileThemeName.IsNull())
                            return mobileThemeName;
                        var themeName = storeStateSettings.DefaultStoredThemeForMobile;
                        if (!themeProvider.ThemeConfigExists(themeName)) // if theme configuration doesn't exist, search all the desktop themes and select one among them
                            themeName = themeProvider.GetThemeConfigurations()
                                .Where(x => x.IsForMobile).FirstOrDefault().ThemeName; // only get the first theme matched, if there is no theme, should throw exception because shouldn't allow site without theme

                        mobileThemeName = themeName;
                        return themeName;
                    }
                case WorkType.Desktop:
                default:
                    {
                        if (!desktopThemeName.IsNull())
                            return desktopThemeName;
                        var themeName = workContext.CurrentUser.GetWorkingDesktopThemeName(UserCharacteristicResource.DesktopThemeName);
                        if (!themeProvider.ThemeConfigExists(themeName)) // if theme configuration doesn't exist, search all the desktop themes and select one among them
                            themeName = themeProvider.GetThemeConfigurations()
                                .Where(x => !x.IsForMobile).FirstOrDefault().ThemeName; // only get the first theme matched, if there is no theme, should throw exception because shouldn't allow site without theme

                        desktopThemeName = themeName;
                        return themeName;
                    }
            }
        }

        public bool SetTheme(string themeName, WorkType type = WorkType.Desktop)
        {
            switch (type)
            {
                case WorkType.Mobile:
                    {
                        return false; // Cannot support set mobile theme by user so far.
                    }
                case WorkType.Desktop:
                default:
                    {
                        if (!storeStateSettings.SelectThemeByUsersIsAllowed)
                            return false;
                        if (workContext.CurrentUser.IsNull())
                            return false;
                        bool succeed = genericCharacteristicService.SaveCharacteristic(workContext.CurrentUser, UserCharacteristicResource.DesktopThemeName, themeName);
                        if(succeed)
                            desktopThemeName = null; // clear
                        return succeed;
                    }
            }   
        }
    }
}
