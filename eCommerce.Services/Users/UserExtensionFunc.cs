﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Services.WcfClient.Entities;
using eCommerce.Core.Common;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Data;

namespace eCommerce.Services.Users
{
    public static class UserExtensionFunc
    {
        public static T GetCharacteristic<T>(this User user, string key)
        {
            return AspectF.Define.MustBeNonNull(user)
                .Return<T>(() =>
            {
                string characteristicValue;
                using (UserExtensionClient proxy = new UserExtensionClient("BasicHttpBinding_IUserExtension"))
                {
                    characteristicValue = proxy.GetCharacteristicValue(user.Id, key);
                }

                if (String.IsNullOrEmpty(characteristicValue))
                {
                    return default(T);
                }
                else
                {
                    if (typeof(T).GetTypeConverter().CanConvertFrom(typeof(string)))
                        throw new Exception("Cannot get type converter");
                    return (T)typeof(T).GetTypeConverter().ConvertFromInvariantString(characteristicValue);
                }
            });
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
            return userSettings.ProvideUserEmail ? user.Email : user.UserName;
        }
    }
}
