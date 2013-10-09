using eCommerce.Core;
using eCommerce.Data.Common;

namespace eCommerce.Data.Domain.Users.Entities
{
    public partial class UserRole : EntityBase
    {
        public string RoleName { get; set; } // role name
        public string SystemName { get; set; } // the name in system role group [registered],[guest]
        public bool IsSystemRole { get; set; } // is system role or not
        public bool Actived { get; set; } // indicate if the user role is actived or disabled
    }

    public sealed class MissingUserRole : UserRole
    {
    }

    public class EmptyUserRoleMap : EmptyEntityMap<UserRole>
    {
        public override UserRole Get()
        {
            return new MissingUserRole();
        }
    }
}
