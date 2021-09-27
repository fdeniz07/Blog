using System;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.Configurations
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Content).IsRequired().HasColumnType("NVARCHAR(MAX)");
            builder.Property(c => c.CreatedByName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);

            //Bir makeleye ait birden fazla yorum yapilabilir.
            builder.HasOne<Blog>(c => c.Blog).WithMany(b => b.Comments).HasForeignKey(c => c.BlogId);

            builder.ToTable("Comments");

            //builder.HasData(
            //   new Comment
            //   {
            //       //DB olusturulmadan önce data olusturulacaksa mutlaka Id girilmelidir. Sonradan girilecek Id degeri girmeye gerek yoktur.
            //       Id = 1,
            //       BlogId = 1,
            //       Content = "Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz.",
            //       IsActive = true,
            //       IsDeleted = false,
            //       CreatedByName = "InitialCreate", //InitialCreate degerini Db nin olusturacagini belirtiyoruz
            //       CreatedDate = DateTime.Now,
            //       ModifiedByName = "InitialCreate",
            //       ModifiedDate = DateTime.Now,
            //       Note = "C# Makale Yorumu"
            //   },
            //   new Comment
            //   {
            //       Id = 2,
            //       BlogId = 2,
            //       Content = "Lorem Ipsum sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır.",
            //       IsActive = true,
            //       IsDeleted = false,
            //       CreatedByName = "InitialCreate", //InitialCreate degerini Db nin olusturacagini belirtiyoruz
            //       CreatedDate = DateTime.Now,
            //       ModifiedByName = "InitialCreate",
            //       ModifiedDate = DateTime.Now,
            //       Note = "Java Makale Yorumu"
            //   },
            //   new Comment
            //   {
            //       Id = 3,
            //       BlogId = 3,
            //       Content = "Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir.",
            //       IsActive = true,
            //       IsDeleted = false,
            //       CreatedByName = "InitialCreate",
            //       CreatedDate = DateTime.Now,
            //       ModifiedByName = "InitialCreate",
            //       ModifiedDate = DateTime.Now,
            //       Note = "Java Script Makale Yorumu"
            //   });
        }
    }
}
