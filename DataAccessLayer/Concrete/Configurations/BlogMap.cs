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
            builder.Property(b => b.Title).IsRequired().HasMaxLength(30);
            builder.Property(b => b.Content).IsRequired().HasColumnType("NVARCHAR(MAX)");
            builder.Property(b => b.Date).IsRequired();
            builder.Property(b => b.Image).HasMaxLength(250);
            builder.Property(b => b.ThumbnailImage).IsRequired().HasMaxLength(250);
            builder.Property(b => b.SeoAuthor).IsRequired().HasMaxLength(50);
            builder.Property(b => b.SeoDescription).IsRequired().HasMaxLength(150);
            builder.Property(b => b.SeoTags).IsRequired().HasMaxLength(100);
            builder.Property(b => b.ViewsCount).IsRequired();
            builder.Property(b => b.CommentCount).IsRequired();
            builder.Property(b => b.CreatedByName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.ModifiedByName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.CreatedDate).IsRequired();
            builder.Property(b => b.ModifiedDate).IsRequired();
            builder.Property(b => b.IsActive).IsRequired();
            builder.Property(b => b.IsDeleted).IsRequired();
            builder.Property(b => b.Note).HasMaxLength(500);

            //Bir kategorinin birden fazla makalesi olabilir
            builder.HasOne<Category>(b => b.Category).WithMany(c => c.Blogs).HasForeignKey(a => a.CategoryId);

            //Bir kullanicinin birden fazla makalesi olabilir
            builder.HasOne<User>(b => b.User).WithMany(u => u.Blogs).HasForeignKey(b => b.UserId);

            builder.ToTable("Blogs");
        }
    }
}
