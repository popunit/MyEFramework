using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Data.Domain.Users.Entities;

namespace eCommerce.Data.Mappings.Users
{
    public partial class UserCharacteristicMap : EntityTypeConfiguration<UserCharacteristic>
    {
        public UserCharacteristicMap()
        {
            #region Table Name
            this.ToTable("UserCharacteristic");
            #endregion

            #region Table Keys
            this.HasKey(uc => uc.Id);
            #endregion

            #region Table Colume Settings
            this.Property(uc => uc.Key).IsRequired().HasMaxLength(255);
            this.Property(uc => uc.Value).IsRequired().HasMaxLength(255);

            this.HasRequired(uc => uc.User)
                .WithMany(user => user.UserCharacteristics)
                .HasForeignKey(uc => uc.UserId);
            #endregion
        }
    }
}
