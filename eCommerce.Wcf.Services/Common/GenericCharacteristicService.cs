using eCommerce.Core;
using eCommerce.Core.Caching;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Data.Common;
using eCommerce.Data.Domain.Common.Entities;
using eCommerce.Wcf.Services.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Wcf.Services.Common
{
    public class GenericCharacteristicService : IGenericCharacteristicService
    {
        private readonly IRepository<GenericCharacteristic> genericCharacteristicRepository;
        private readonly ICacheManager cacheManager;
        //private readonly ILifetimeScope container;

        public GenericCharacteristicService()
        {
            //this.container = AutofacHostFactory.Container;
            this.genericCharacteristicRepository = 
                EngineContext.Current.Resolve<IRepository<GenericCharacteristic>>();
            this.cacheManager = EngineContext.Current.Resolve<ICacheManager>();
        }

        public bool InsertCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).Return<bool>(() => 
            {
                bool succeed = genericCharacteristicRepository.Insert(characteristic);
                if (succeed)
                    cacheManager.RemoveByPattern(Constants.CACHE_GENERICCHARACTERISTIC_PATTERN);

                // event notification

                return succeed;
            });
        }

        public GenericCharacteristic GetCharacteristicById(long characteristicId)
        {
            return AspectF.Define.MustBeNonDefault<long>(characteristicId).Return<GenericCharacteristic>(() => 
            {
                return genericCharacteristicRepository.GetByKeys(characteristicId);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="chGroup"></param>
        /// <returns>[TO-DO] Considering using Collection Data Contract to substitude</returns>
        public IEnumerable<GenericCharacteristic> GetCharacteristicForEntity(long entityId, string chGroup)
        {
            string key = string.Format(Constants.CACHE_GENERICCHARACTERISTIC_FORMAT, entityId, chGroup);
            return cacheManager.GetOrAdd(
                key,
                () => 
                {
                    var results = from ch in genericCharacteristicRepository.Table
                                  where ch.EntityId == entityId &&
                                  ch.Group == chGroup
                                  select ch;
                    return results;
                });
        }

        public bool UpdateCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).Return<bool>(() => 
            {
                var succeed = genericCharacteristicRepository.Update(characteristic);
                if (succeed)
                    cacheManager.RemoveByPattern(Constants.CACHE_GENERICCHARACTERISTIC_PATTERN);

                // event notification

                return succeed;
            });
        }

        public bool DeleteCharacteristic(GenericCharacteristic characteristic)
        {
            return AspectF.Define.MustBeNonNull(characteristic).Return<bool>(() => 
            {
                var succeed = genericCharacteristicRepository.Delete(characteristic);
                if (succeed)
                    cacheManager.RemoveByPattern(Constants.CACHE_GENERICCHARACTERISTIC_PATTERN);

                // event notification

                return succeed;
            });
        }

        public bool SaveCharacteristic(EntityBase entity, string key, string value)
        {
            return AspectF.Define.MustBeNonNull(entity).MustBeNonNullOrEmpty(key).Return<bool>(() => 
            {
                string group = entity.GetUnproxyType().Name; // real type name

                var chs = GetCharacteristicForEntity(entity.Id, group);
                var ch = chs.FirstOrDefault(gch => gch.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));

                if (null != ch)
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return DeleteCharacteristic(ch);
                    }
                    else
                    {
                        ch.Value = value;
                        return UpdateCharacteristic(ch);
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        ch = new GenericCharacteristic(); // create new characteristic
                        ch.EntityId = entity.Id;
                        ch.Key = key;
                        ch.Value = value;
                        ch.Group = group;

                        return InsertCharacteristic(ch);
                    }
                }

                return false; // no data to be saved
            });
        }
    }
}
