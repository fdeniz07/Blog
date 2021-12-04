using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.Mapping
{
    public class RoleClaimMap:IEntityTypeConfiguration<RoleClaim>
    {
        #region Implementation of IEntityTypeConfiguration<RoleClaim>

        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            // Primary key
            builder.HasKey(rc => rc.Id);

            // Maps to the AspNetRoleClaims table
            builder.ToTable("RoleClaims");
        }

        #endregion
    }
}
