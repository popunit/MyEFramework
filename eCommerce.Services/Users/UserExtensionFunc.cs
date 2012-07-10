using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Services.WcfClient.Entities;
using eCommerce.Core.Common;
using eCommerce.Core.Infrastructure.NoAOP;

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

    }
}
