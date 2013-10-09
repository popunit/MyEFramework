using eCommerce.Data.Domain.Users.Entities;
using System.Data.Entity.ModelConfiguration;

namespace eCommerce.Data.Mappings.Users
{
    public partial class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            #region Table Name
            this.ToTable("User");
            #endregion

            #region Table Keys
            this.HasKey(user => user.Id);
            #endregion

            #region Table Colume Settings
            this.Property(user => user.UserName).HasMaxLength(50).IsRequired();
            this.Property(user => user.Email).HasMaxLength(255);
            this.Property(user => user.PasswordKit.Password).HasMaxLength(255).HasColumnName("Password").IsRequired();

            //this.Ignore(user => user.PasswordKit.PasswordFormatType); // WCF Data Service doesn't support this ignore
            #endregion

            #region Table Relationships
            this.HasMany(u => u.UserRoles).WithMany().Map(t => t.ToTable("User_UserRole_View")); // many to many, join tables (user and userrole)
            #endregion
        }
    }
}
