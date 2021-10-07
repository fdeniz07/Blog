using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccessLayer.Concrete.Configurations
{
    public class UserClaimMap:IEntityTypeConfiguration<UserClaim>
    {
        #region Implementation of IEntityTypeConfiguration<UserClaim>

        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            // Primary key
            builder.HasKey(uc => uc.Id);

            // Maps to the AspNetUserClaims table
            builder.ToTable("UserClaims");
        }

        #endregion
    }
}
