using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Data.Domain.Users.Entities;

namespace eCommerce.Data.Mappings.Users
{
    public partial class UserMap : EntityTypeConfiguration<User>
    {
    }
}
