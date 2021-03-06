﻿using System;
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
            this.UserGuid = Guid.NewGuid(); // set guid automatically
            PasswordKit = new PasswordKit();
        }

        public Guid UserGuid { get; set; } // used for authenticated users and guest
        public string UserName { get; set; }
        public PasswordKit PasswordKit { get; set; }
        public string Email { get; set; }

        // user status
        public bool Actived { get; set; } // indicate if the user is actived or disabled
        public bool Deleted { get; set; } // indicate if the user is deleted

        // time record
        public DateTime CreateTime { get; set; }
        public DateTime ActiveTime { get; set; }
        public DateTime DeleteTime { get; set; }

        // Navigation properties : Nullable
        // many to many
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    public sealed class MissingUser : User
    { 
    }

    public class EmptyUserMap : EmptyEntityMap<User>
    {
        public override User Get()
        {
            return new MissingUser();
        }
    }
}
