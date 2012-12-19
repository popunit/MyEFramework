using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Data.Domain.Users.Entities;

namespace eCommerce.Data.Mappings.Users
{
    public partial class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            #region Table Name
            this.ToTable("UserRole");
            #endregion

            #region Table Keys
            this.HasKey(role => role.Id);
            #endregion

            #region Table Colume Settings
            this.Property(role => role.RoleName).HasMaxLength(255).IsRequired();
            this.Property(role => role.SystemName).HasMaxLength(255);
            #endregion
        }
    }
}
