using eCommerce.Data.Domain.Common.Entities;
using System.Data.Entity.ModelConfiguration;

namespace eCommerce.Data.Mappings.Common
{
    public partial class GenericCharacteristicMap : EntityTypeConfiguration<GenericCharacteristic>
    {
        public GenericCharacteristicMap()
        {
            #region Table Name
            this.ToTable("GenericCharacteristic");
            #endregion

            #region Table Keys
            this.HasKey(gc => gc.Id);
            #endregion

            #region Table Colume Settings
            this.Property(gc => gc.Key).IsRequired().HasMaxLength(255);
            this.Property(gc => gc.Value).IsRequired(); // value is required, that means we cannot get empty or null from the column
            this.Property(gc => gc.Group).IsRequired().HasMaxLength(255);
            #endregion
        }
    }
}
