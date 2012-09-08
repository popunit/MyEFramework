using eCommerce.Core;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Services.WcfClient;
using eCommerce.Services.WcfClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.Common
{
    public class GenericCharacteristicDataService : IGenericCharacteristicDataService
    {
        public bool InsertCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).Return<bool>(() => 
            {
                bool isSucceed = false;
                // [TO-DO] To use WCF discovery to substitude
                using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                {
                    isSucceed = proxy.InsertCharacteristic(characteristic);
                }

                return isSucceed;
            });
        }

        public GenericCharacteristic GetCharacteristicById(long characteristicId)
        {
            return AspectF.Define.MustBeNonDefault(characteristicId).Return<GenericCharacteristic>(() =>
            {
                GenericCharacteristic result = null;
                // [TO-DO] To use WCF discovery to substitude
                using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                {
                    result = proxy.GetCharacteristicById(characteristicId);
                }

                return result;
            });
        }

        public IEnumerable<GenericCharacteristic> GetCharacteristicForEntity(long entityId, string group)
        {
            return AspectF.Define.MustBeNonDefault(entityId).Return<IEnumerable<GenericCharacteristic>>(() =>
            {
                IEnumerable<GenericCharacteristic> results = null;
                // [TO-DO] To use WCF discovery to substitude
                using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                {
                    results = proxy.GetCharacteristicForEntity(entityId, group);
                }

                return results;
            });
        }

        public bool UpdateCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).Return<bool>(() =>
            {
                bool isSucceed = false;
                // [TO-DO] To use WCF discovery to substitude
                using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                {
                    isSucceed = proxy.UpdateCharacteristic(characteristic);
                }

                return isSucceed;
            });
        }

        public bool DeleteCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).Return<bool>(() =>
            {
                bool isSucceed = false;
                // [TO-DO] To use WCF discovery to substitude
                using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                {
                    isSucceed = proxy.DeleteCharacteristic(characteristic);
                }

                return isSucceed;
            });
        }

        public bool SaveCharacteristic(EntityBase entity, string key, string value)
        {
            return AspectF.Define.MustBeNonNull(entity).MustBeNonNullOrEmpty(key).Return<bool>(() =>
            {
                bool isSucceed = false;
                // [TO-DO] To use WCF discovery to substitude
                using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                {
                    isSucceed = proxy.SaveCharacteristic(entity, key, value);
                }

                return isSucceed;
            });
        }
    }
}
