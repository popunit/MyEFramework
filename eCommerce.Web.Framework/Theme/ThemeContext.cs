using eCommerce.Core.Common;
using eCommerce.Core.Data;
using eCommerce.Core.Enums;
using eCommerce.Data.Resources;
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
        private readonly WebWorkContextBase _workContext;
        private readonly IGenericCharacteristicDataService _genericCharacteristicService;
        private readonly StoreStateSettings _storeStateSettings;
        private readonly IThemeProvider _themeProvider;

        private string _desktopThemeName;
        private string _mobileThemeName;

        public ThemeContext(
            WebWorkContextBase workContext,
            IGenericCharacteristicDataService genericCharacteristicService,
            StoreStateSettings storeStateSettings,
            IThemeProvider themeProvider
            )
        {
            this._workContext = workContext;
            this._genericCharacteristicService = genericCharacteristicService;
            this._storeStateSettings = storeStateSettings;
            this._themeProvider = themeProvider;

            // initial theme name using null, represent there is no theme name cached.
            _desktopThemeName = null;
            _mobileThemeName = null;
        }

        public string GetCurrentTheme(WorkType type)
        {
            switch (type)
            {
                case WorkType.Mobile:
                    {
                        if (!_mobileThemeName.IsNull())
                            return _mobileThemeName;
                        var themeName = _storeStateSettings.DefaultStoredThemeForMobile;
                        if (!_themeProvider.ThemeConfigExists(themeName)) // if theme configuration doesn't exist, search all the desktop themes and select one among them
                            themeName = _themeProvider.GetThemeConfigurations()
                                .Where(x => x.IsForMobile).FirstOrDefault().ThemeName; // only get the first theme matched, if there is no theme, should throw exception because shouldn't allow site without theme

                        _mobileThemeName = themeName;
                        return themeName;
                    }
                case WorkType.Desktop:
                default:
                    {
                        if (!_desktopThemeName.IsNull())
                            return _desktopThemeName;
                        var themeName = _workContext.CurrentUser.GetWorkingDesktopThemeName(UserCharacteristicResource.DesktopThemeName);
                        if (!_themeProvider.ThemeConfigExists(themeName)) // if theme configuration doesn't exist, search all the desktop themes and select one among them
                            themeName = _themeProvider.GetThemeConfigurations()
                                .Where(x => !x.IsForMobile).FirstOrDefault().ThemeName; // only get the first theme matched, if there is no theme, should throw exception because shouldn't allow site without theme

                        _desktopThemeName = themeName;
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
                        if (!_storeStateSettings.SelectThemeByUsersIsAllowed)
                            return false;
                        if (_workContext.CurrentUser.IsNull())
                            return false;
                        bool succeed = _genericCharacteristicService.SaveCharacteristic(_workContext.CurrentUser, UserCharacteristicResource.DesktopThemeName, themeName);
                        if(succeed)
                            _desktopThemeName = null; // clear
                        return succeed;
                    }
            }   
        }
    }
}
