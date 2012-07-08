using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Data.Common;

namespace eCommerce.Data.Domain.Users.Entities
{
    public partial class User : EntityBase
    {
        public User()
        {
            this.UserGuid = new Guid();
            PasswordKit = new PasswordKit();
        }

        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public PasswordKit PasswordKit { get; set; }
        public string Email { get; set; }

        // Navigation properties : Nullable
        public virtual ICollection<UserCharacteristic> UserCharacteristics { get; protected set; }
    }

    public sealed class MissingUser : User
    { 
    }

    internal class EmptyUserMap : EmptyEntityMap<User>
    {
        public override User Get()
        {
            return new MissingUser();
        }
    }
}
