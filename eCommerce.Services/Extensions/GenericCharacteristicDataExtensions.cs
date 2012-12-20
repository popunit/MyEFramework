using eCommerce.Core;
using eCommerce.Core.Common;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Exception;
using eCommerce.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.Extensions
{
    public static class GenericCharacteristicDataExtensions
    {
        public static T GetCharacteristic<T>(this EntityBase entity, string key, IGenericCharacteristicDataService gcService)
        {
            return AspectF.Define.MustBeNonNull(entity).Return<T>(() => 
            {
                var entityType = entity.GetType().Name; // if get entity type in database server, should call unproxy type for real type
                var results = gcService.GetCharacteristicForEntity(entity.Id, entityType);
                if (results.IsNull() || results.Count() == 0)
                    return default(T);
                var result = results.FirstOrDefault(gc =>
                    gc.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));

                if (result.IsNull() || string.IsNullOrEmpty(result.Value))
                    return default(T);

                // [TO-DO] Convert
                if (typeof(T).GetTypeConverter().CanConvertFrom(typeof(string)))
                    throw new CommonException("Cannot get type converter");
                return (T)typeof(T).GetTypeConverter().ConvertFromInvariantString(result.Value);
            });
        }

        public static T GetCharacteristic<T>(this EntityBase entity, string key)
        {
            var gcService = EngineContext.Current.Resolve<IGenericCharacteristicDataService>();
            return GetCharacteristic<T>(entity, key, gcService);
        }
    }
}
