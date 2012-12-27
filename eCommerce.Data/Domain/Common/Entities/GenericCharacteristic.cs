using eCommerce.Core;
using eCommerce.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.Domain.Common.Entities
{
    /// <summary>
    /// Generic characteristic, different from user characteristic
    /// </summary>
    /// <remarks>Consider change the class name to EntityCharacteristic</remarks>
    public partial class GenericCharacteristic : EntityBase
    {
        public long EntityId { get; set; } // Domain Entity Id
        public string Group { get; set; } // Domain Entity type, EntityId and Group can specify the unique entity 

        public string Key { get; set; }
        public string Value { get; set; }
    }

    public sealed class MissingGenericCharacteristic : GenericCharacteristic
    {
    }

    public class EmptyGenericCharacteristicMap : EmptyEntityMap<GenericCharacteristic>
    {
        public override GenericCharacteristic Get()
        {
            return new MissingGenericCharacteristic();
        }
    }
}
