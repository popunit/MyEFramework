using eCommerce.Core;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Exception;
using eCommerce.Services.WcfClient;
using eCommerce.Services.WcfClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Services.Extensions.NoAOP;

namespace eCommerce.Services.Common
{
    public class GenericCharacteristicDataService : IGenericCharacteristicDataService
    {
        public bool InsertCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).
                WcfClient<IGenericCharacteristicService>().Return<bool>((aspect) =>
            {
                return aspect.Proxy.InsertCharacteristic(characteristic);
            });
        }

        public GenericCharacteristic GetCharacteristicById(long characteristicId)
        {
            return AspectF.Define.MustBeNonDefault(characteristicId).
                WcfClient<IGenericCharacteristicService>().Return<GenericCharacteristic>((aspect) =>
            {
                return aspect.Proxy.GetCharacteristicById(characteristicId);
            });
        }

        public IEnumerable<GenericCharacteristic> GetCharacteristicForEntity(long entityId, string group)
        {
            return AspectF.Define.MustBeNonDefault(entityId).
                WcfClient<IGenericCharacteristicService>().Return<IEnumerable<GenericCharacteristic>>((aspect) =>
            {
                return aspect.Proxy.GetCharacteristicForEntity(entityId, group);
            });
        }

        public bool UpdateCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).
                WcfClient<IGenericCharacteristicService>().Return<bool>((aspect) =>
            {
                return aspect.Proxy.UpdateCharacteristic(characteristic);
            });
        }

        public bool DeleteCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).
                WcfClient<IGenericCharacteristicService>().Return<bool>((aspect) =>
            {
                return aspect.Proxy.DeleteCharacteristic(characteristic);
            });
        }

        public bool SaveCharacteristic(EntityBase entity, string key, string value)
        {
            return AspectF.Define.MustBeNonNull(entity).MustBeNonNullOrEmpty(key).
                WcfClient<IGenericCharacteristicService>().Return<bool>((aspect) =>
            {
                return aspect.Proxy.SaveCharacteristic(entity, key, value);
            });
        }
    }
}
