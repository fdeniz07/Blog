using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.Configurations
{
    public class BlogMap : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).UseIdentityColumn();
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.Image).HasMaxLength(250);
            builder.Property(b => b.Content).IsRequired().HasMaxLength(500);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(30);
            builder.Property(b => b.Image).HasMaxLength(250);
            builder.Property(b => b.ThumbnailImage).HasMaxLength(250);
            builder.Property(b => b.CreatedByName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.ModifiedByName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.CreatedDate).IsRequired();
            builder.Property(b => b.ModifiedDate).IsRequired();
            builder.Property(b => b.IsActive).IsRequired();
            builder.Property(b => b.IsDeleted).IsRequired();
            builder.Property(b => b.Note).HasMaxLength(500);
        }
    }
}
