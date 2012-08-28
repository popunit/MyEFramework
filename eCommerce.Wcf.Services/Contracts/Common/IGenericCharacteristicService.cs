using eCommerce.Core;
using eCommerce.Data.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Wcf.Services.Contracts.Common
{
    /// <summary>
    /// CRUD
    /// </summary>
    public interface IGenericCharacteristicService
    {
        bool InsertCharacteristic(GenericCharacteristic characteristic);
        GenericCharacteristic GetCharacteristicById(int characteristicId);
        IEnumerable<GenericCharacteristic> GetCharacteristicForEntity(int entityId, string group);
        bool UpdateCharacteristic(GenericCharacteristic characteristic);
        bool DeleteCharacteristic(GenericCharacteristic characteristic);
        bool SaveCharacteristic(EntityBase entity, string key, string value);
    }
}
