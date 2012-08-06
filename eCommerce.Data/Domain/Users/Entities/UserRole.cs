using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Data.Common;

namespace eCommerce.Data.Domain.Users.Entities
{
    public partial class UserRole : EntityBase
    {
        public string RoleName { get; set; }
        public bool IsSystemRole { get; set; }
        public bool Actived { get; set; } // indicate if the user role is actived or disabled
    }

    public sealed class MissingUserRole : UserRole
    {
    }

    internal class EmptyUserRoleMap : EmptyEntityMap<UserRole>
    {
        public override UserRole Get()
        {
            return new MissingUserRole();
        }
    }
}
