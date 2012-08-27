using eCommerce.Core;
using eCommerce.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.Domain.Common
{
    /// <summary>
    /// Generic characteristic, different from user characteristic
    /// </summary>
    public partial class GenericCharacteristic : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>doesn't associate with specific type entities</remarks>
        public long EntityId { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
    }

    public sealed class MissingGenericCharacteristic : GenericCharacteristic
    {
    }

    internal class EmptyGenericCharacteristicMap : EmptyEntityMap<GenericCharacteristic>
    {
        public override GenericCharacteristic Get()
        {
            return new MissingGenericCharacteristic();
        }
    }
}
