using eCommerce.Data.Domain.Users.Entities;
using System.Data.Entity.ModelConfiguration;

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
