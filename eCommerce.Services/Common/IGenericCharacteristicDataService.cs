using eCommerce.Core;
using eCommerce.Services.WcfClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.Common
{
    public interface IGenericCharacteristicDataService
    {
        bool InsertCharacteristic(GenericCharacteristic characteristic);
        GenericCharacteristic GetCharacteristicById(long characteristicId);
        IEnumerable<GenericCharacteristic> GetCharacteristicForEntity(long entityId, string group);
        bool UpdateCharacteristic(GenericCharacteristic characteristic);
        bool DeleteCharacteristic(GenericCharacteristic characteristic);
        bool SaveCharacteristic(EntityBase entity, string key, string value);
    }
}
