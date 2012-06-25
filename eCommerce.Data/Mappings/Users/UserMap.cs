﻿using System;
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
        public UserMap()
        {
            #region Table Name
            this.ToTable("User");
            #endregion

            #region Table Keys
            this.HasKey(user => user.Id);
            #endregion

            #region Table Colume Settings
            this.Property(user => user.UserName).HasMaxLength(50);
            this.Property(user => user.Email).HasMaxLength(255);
            #endregion
        }
    }
}
