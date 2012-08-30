using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Data.Common;

namespace eCommerce.Data.Domain.Users.Entities
{
    /// <summary>
    /// [TO-DO] Deprecated. Functions move to GenericCharacteristic
    /// </summary>
    public partial class UserCharacteristic : EntityBase
    {
        // Foreign key (to User.Id)
        public long UserId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        // Navigation properties : one to zero or many
        public virtual User User { get; set; }
    }

    public sealed class MissingUserCharacteristic : UserCharacteristic
    { 
    }

    internal class EmptyUserCharacteristicMap : EmptyEntityMap<UserCharacteristic>
    {
        public override UserCharacteristic Get()
        {
            return new MissingUserCharacteristic();
        }
    }
}
