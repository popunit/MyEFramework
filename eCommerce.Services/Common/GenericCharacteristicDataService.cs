using eCommerce.Core;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Services.WcfClient;
using eCommerce.Services.WcfClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
                //bool isSucceed = false;
                //// [TO-DO] To use WCF discovery to substitude
                //using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                //{
                //    isSucceed = proxy.InsertCharacteristic(characteristic);
                //}

                //return isSucceed;

                bool isSucceed = false;

                var proxy = ProxyFactory.Create<IGenericCharacteristicService, BasicHttpBinding>();
                try
                {
                    isSucceed = proxy.InsertCharacteristic(characteristic);
                    return isSucceed;
                }
                finally
                {
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
            });
        }

        public GenericCharacteristic GetCharacteristicById(long characteristicId)
        {
            return AspectF.Define.MustBeNonDefault(characteristicId).Return<GenericCharacteristic>(() =>
            {
                //GenericCharacteristic result = null;
                //// [TO-DO] To use WCF discovery to substitude
                //using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                //{
                //    result = proxy.GetCharacteristicById(characteristicId);
                //}

                //return result;

                GenericCharacteristic result = null;
                var proxy = ProxyFactory.Create<IGenericCharacteristicService, BasicHttpBinding>();
                try
                {
                    result = proxy.GetCharacteristicById(characteristicId);
                    return result;
                }
                finally
                {
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
            });
        }

        public IEnumerable<GenericCharacteristic> GetCharacteristicForEntity(long entityId, string group)
        {
            return AspectF.Define.MustBeNonDefault(entityId).Return<IEnumerable<GenericCharacteristic>>(() =>
            {
                //IEnumerable<GenericCharacteristic> results = null;
                //// [TO-DO] To use WCF discovery to substitude
                //using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                //{
                //    results = proxy.GetCharacteristicForEntity(entityId, group);
                //}

                //return results;

                IEnumerable<GenericCharacteristic> results = null;
                var proxy = ProxyFactory.Create<IGenericCharacteristicService, BasicHttpBinding>();
                try
                {
                    results = proxy.GetCharacteristicForEntity(entityId, group);
                    return results;
                }
                finally
                {
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
            });
        }

        public bool UpdateCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).Return<bool>(() =>
            {
                //bool isSucceed = false;
                //// [TO-DO] To use WCF discovery to substitude
                //using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                //{
                //    isSucceed = proxy.UpdateCharacteristic(characteristic);
                //}

                //return isSucceed;

                bool isSucceed = false;

                var proxy = ProxyFactory.Create<IGenericCharacteristicService, BasicHttpBinding>();
                try
                {
                    isSucceed = proxy.UpdateCharacteristic(characteristic);
                    return isSucceed;
                }
                finally
                {
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
            });
        }

        public bool DeleteCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).Return<bool>(() =>
            {
                //bool isSucceed = false;
                //// [TO-DO] To use WCF discovery to substitude
                //using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                //{
                //    isSucceed = proxy.DeleteCharacteristic(characteristic);
                //}

                //return isSucceed;

                bool isSucceed = false;

                var proxy = ProxyFactory.Create<IGenericCharacteristicService, BasicHttpBinding>();
                try
                {
                    isSucceed = proxy.DeleteCharacteristic(characteristic);
                    return isSucceed;
                }
                finally
                {
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
            });
        }

        public bool SaveCharacteristic(EntityBase entity, string key, string value)
        {
            return AspectF.Define.MustBeNonNull(entity).MustBeNonNullOrEmpty(key).Return<bool>(() =>
            {
                //bool isSucceed = false;
                //// [TO-DO] To use WCF discovery to substitude
                //using (var proxy = new GenericCharacteristicServiceClient("BasicHttpBinding_IGenericCharacteristicService"))
                //{
                //    isSucceed = proxy.SaveCharacteristic(entity, key, value);
                //}

                //return isSucceed;

                bool isSucceed = false;

                var proxy = ProxyFactory.Create<IGenericCharacteristicService, BasicHttpBinding>();
                try
                {
                    isSucceed = proxy.SaveCharacteristic(entity, key, value);
                    return isSucceed;
                }
                finally
                {
                    if (null != proxy)
                        (proxy as ICommunicationObject).Close();
                }
            });
        }
    }
}
