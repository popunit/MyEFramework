﻿using eCommerce.Core;
using eCommerce.Data.Domain.Common.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace eCommerce.Wcf.Services.Contracts.Common
{
    /// <summary>
    /// CRUD
    /// </summary>
    [ServiceContract]
    public interface IGenericCharacteristicService
    {
        [OperationContract]
        bool InsertCharacteristic(GenericCharacteristic characteristic);

        [OperationContract]
        GenericCharacteristic GetCharacteristicById(long characteristicId);

        [OperationContract]
        IEnumerable<GenericCharacteristic> GetCharacteristicForEntity(long entityId, string group);

        [OperationContract]
        bool UpdateCharacteristic(GenericCharacteristic characteristic);

        [OperationContract]
        bool DeleteCharacteristic(GenericCharacteristic characteristic);

        [OperationContract]
        bool SaveCharacteristic(EntityBase entity, string key, string value);
    }
}
