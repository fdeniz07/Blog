using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.Mapping
{
    public class AboutMap : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).UseIdentityColumn();
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Details1).IsRequired().HasMaxLength(500);
            builder.Property(a => a.Details2).HasMaxLength(500);
            builder.Property(a => a.Image1).HasMaxLength(250);
            builder.Property(a => a.Image2).HasMaxLength(250);
            builder.Property(a => a.MapLocation).HasMaxLength(250);
            builder.Property(a => a.CreatedByName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.ModifiedByName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(500);
        }
    }
}
