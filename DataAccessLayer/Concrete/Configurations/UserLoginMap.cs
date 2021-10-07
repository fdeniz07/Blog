﻿using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.Configurations
{
    public class UserLoginMap:IEntityTypeConfiguration<UserLogin>
    {
        #region Implementation of IEntityTypeConfiguration<UserLogin>

        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            // Composite primary key consisting of the LoginProvider and the key to use
            // with that provider
            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Limit the size of the composite key columns due to common DB restrictions
            builder.Property(l => l.LoginProvider).HasMaxLength(128);
            builder.Property(l => l.ProviderKey).HasMaxLength(128);

            // Maps to the AspNetUserLogins table
            builder.ToTable("UserLogins");
        }

        #endregion
    }
}
