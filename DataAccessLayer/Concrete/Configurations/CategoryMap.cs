using System;
using System.Text;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.Configurations
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Description).HasMaxLength(150);
            builder.Property(c => c.CreatedByName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);

            builder.ToTable("Categories");

            //Manuel olarak ilk örnek verimizi eklemek istersek;

            builder.HasData(
                new Category
                {
                    //DB olusturulmadan önce data olusturulacaksa mutlaka Id girilmelidir. Sonradan girilecek Id degeri girmeye gerek yoktur.
                    Id = 1,
                    CategoryName = "C#",
                    Description = "C# Programlama Dili ile Ilgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate", //InitialCreate degerini Db nin olusturacagini belirtiyoruz
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C# Blog Kategorisi"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Java",
                    Description = "Java Programlama Dili ile Ilgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Java Blog Kategorisi"
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Java Script",
                    Description = "Java Script Programlama Dili ile Ilgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Java Script Blog Kategorisi"
                });
        }
    }
}
