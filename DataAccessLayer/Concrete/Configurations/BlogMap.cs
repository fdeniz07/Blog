using System;
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
            builder.Property(b => b.Title).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Content).IsRequired().HasColumnType("NVARCHAR(MAX)");
            builder.Property(b => b.Date).IsRequired();
            builder.Property(b => b.Image).HasMaxLength(250);
            builder.Property(b => b.ThumbnailImage).IsRequired().HasMaxLength(250);
            builder.Property(b => b.SeoAuthor).IsRequired().HasMaxLength(50);
            builder.Property(b => b.SeoDescription).IsRequired().HasMaxLength(150);
            builder.Property(b => b.SeoTags).IsRequired().HasMaxLength(100);
            builder.Property(b => b.ViewCount).IsRequired();
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

            //Manuel olarak ilk örnek verimizi eklemek istersek;

            //builder.HasData(
            //    new Blog
            //    {
            //        //DB olusturulmadan önce data olusturulacaksa mutlaka Id girilmelidir. Sonradan girilecek Id degeri girmeye gerek yoktur.
            //        Id = 1,
            //        CategoryId = 1,
            //        Title = "C# 9.0 ve NET 5 Yenilikleri",
            //        Content = "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir.",
            //        ThumbnailImage = "Default.jpg",
            //        Image = "Default.jpg",
            //        SeoDescription = "C# 9.0 ve NET 5 Yenilikleri",
            //        SeoTags = "C#, C# 9, .NET 5, .NET Framework, .NET Core, .NET Core MVC",
            //        SeoAuthor = "Fatih Deniz",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate", //InitialCreate degerini Db nin olusturacagini belirtiyoruz
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "C# 9.0 ve NET 5 Yenilikleri",
            //        UserId = 1,
            //        ViewCount = 100,
            //        CommentCount = 1
            //    },
            //    new Blog
            //    {
            //        Id = 2,
            //        CategoryId = 2,
            //        Title = "Java ve Spring Yenilikleri",
            //        Content = "Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir.",
            //        ThumbnailImage = "Default.jpg",
            //        Image = "Default.jpg",
            //        SeoDescription = "Java ve Spring Yenilikleri",
            //        SeoTags = "Java, Spring, Spring Boot, Lombok, Eclipse, Swagger, Maven",
            //        SeoAuthor = "Fatih Deniz",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "Java ve Spring Yenilikleri",
            //        UserId = 1,
            //        ViewCount = 295,
            //        CommentCount = 1
            //    },
            //    new Blog
            //    {
            //        Id = 3,
            //        CategoryId = 3,
            //        Title = "Java Script ES2019 ve ES2020 Yenilikleri",
            //        Content = "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. ",
            //        ThumbnailImage = "Default.jpg",
            //        Image = "Default.jpg",
            //        SeoDescription = "Java Script ES2019 ve ES2020 Yenilikleri",
            //        SeoTags = "Java Script ES2019, Java Script ES2020",
            //        SeoAuthor = "Fatih Deniz",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "Java Script ES2019 ve ES2020 Yenilikleri",
            //        UserId = 1,
            //        ViewCount = 12,
            //        CommentCount = 1
            //    });
        }
    }
}
