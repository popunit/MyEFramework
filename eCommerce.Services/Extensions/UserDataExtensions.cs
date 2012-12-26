using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Services.WcfClient.Entities;
using eCommerce.Core.Common;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Data;
using eCommerce.Services.WcfClient;
using eCommerce.Data.Resources;

namespace eCommerce.Services.Extensions
{
    public static class UserDataExtensions
    {
        //public static T GetCharacteristic<T>(this User user, string key)
        //{
        //    return AspectF.Define.MustBeNonNull(user)
        //        .Return<T>(() =>
        //    {
        //        string characteristicValue;
        //        using (UserExtensionClient proxy = new UserExtensionClient("BasicHttpBinding_IUserExtension"))
        //        {
        //            characteristicValue = proxy.GetCharacteristicValue(user.Id, key);
        //        }

        //        if (String.IsNullOrEmpty(characteristicValue))
        //        {
        //            return default(T);
        //        }
        //        else
        //        {
        //            if (typeof(T).GetTypeConverter().CanConvertFrom(typeof(string)))
        //                throw new Exception("Cannot get type converter");
        //            return (T)typeof(T).GetTypeConverter().ConvertFromInvariantString(characteristicValue);
        //        }
        //    });
        //}

        public static string GetWorkingDesktopThemeName(this User user, string key)
        {
            string themeName = null;
            var storeStateSettings = EngineContext.Current.Resolve<StoreStateSettings>();
            if (user.IsNull())
                return storeStateSettings.DefaultStoredThemeForDesktop; // if anonymous, return default
            if (!storeStateSettings.IsNull() && storeStateSettings.SelectThemeByUsersIsAllowed) // if user is allowed to select theme
            {
                themeName = user.GetCharacteristic<string>(UserCharacteristicResource.DesktopThemeName); 
            }

            if (string.IsNullOrEmpty(themeName)) // if theme is empty, set it by default theme
                themeName = storeStateSettings.DefaultStoredThemeForDesktop;

            return themeName;
        }

        /// <summary>
        /// Get user name or email according to user settings
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetUserNameOrEmail(this User user)
        {
            if (null == user)
                return null;
            var userSettings = EngineContext.Current.Resolve<UserSettings>();
            return userSettings.UsingUserEmail ? user.Email : user.UserName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="systemRoleName"></param>
        /// <param name="disabledRoleIsIncluded">whether disabled roles are included</param>
        /// <returns></returns>
        public static bool HasRole(this User user, string systemRoleName, bool disabledRoleIsIncluded = false)
        {
            return AspectF.Define.MustBeNonNull(user).MustBeNonNullOrEmpty(systemRoleName)
                .Return<bool>(() =>
            {
                // TO-DO: check if user.userroles is available or not
                return user.UserRoles
                    .Where(role => disabledRoleIsIncluded || role.Actived) // if disabled role is not included, role must be actived
                    .Where(role => role.SystemName.Equals(systemRoleName, StringComparison.InvariantCultureIgnoreCase)) // equal to system name
                    .Count() > 0;
            });
        }

        public static bool HasRegisteredRole(this User user, bool includingDisabledRole = false)
        {
            return HasRole(user, SystemUserRoleNameResource.Registered, includingDisabledRole);
        }

        public static bool IsValid(this User user)
        {
            return user != null && !user.Deleted && user.Actived;
        }
    }
}
