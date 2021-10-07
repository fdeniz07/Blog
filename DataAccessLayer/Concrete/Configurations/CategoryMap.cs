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

            //builder.HasData(
            //    new Category
            //    {
            //        //DB olusturulmadan önce data olusturulacaksa mutlaka Id girilmelidir. Sonradan girilecek Id degeri girmeye gerek yoktur.
            //        Id = 1,
            //        CategoryName = "C#",
            //        Description = "C# Programlama Dili ile Ilgili En Güncel Bilgiler",
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate", //InitialCreate degerini Db nin olusturacagini belirtiyoruz
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "C# Blog Kategorisi"
            //    },
            //    new Category
            //    {
            //        Id = 2,
            //        CategoryName = "Java",
            //        Description = "Java Programlama Dili ile Ilgili En Güncel Bilgiler",
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "Java Blog Kategorisi"
            //    },
            //    new Category
            //    {
            //        Id = 3,
            //        CategoryName = "Java Script",
            //        Description = "Java Script Programlama Dili ile Ilgili En Güncel Bilgiler",
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "Java Script Blog Kategorisi"
            //    });


            builder.HasData(
                new Category
                {
                    Id = 1,
                    CategoryName = "C#",
                    Description = "C# Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C# Blog Kategorisi",
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "C++",
                    Description = "C++ Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C++ Blog Kategorisi",
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "JavaScript",
                    Description = "JavaScript Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "JavaScript Blog Kategorisi",
                },
                new Category
                {
                    Id = 4,
                    CategoryName = "Typescript",
                    Description = "Typescript Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Typescript Blog Kategorisi",
                },
                new Category
                {
                    Id = 5,
                    CategoryName = "Java",
                    Description = "Java Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Java Blog Kategorisi",
                },
                new Category
                {
                    Id = 6,
                    CategoryName = "Python",
                    Description = "Python Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Python Blog Kategorisi",
                },
                new Category
                {
                    Id = 7,
                    CategoryName = "Php",
                    Description = "Php Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Php Blog Kategorisi",
                },
                new Category
                {
                    Id = 8,
                    CategoryName = "Kotlin",
                    Description = "Kotlin Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Kotlin Blog Kategorisi",
                },
                new Category
                {
                    Id = 9,
                    CategoryName = "Swift",
                    Description = "Swift Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Swift Blog Kategorisi",
                },
                new Category
                {
                    Id = 10,
                    CategoryName = "Ruby",
                    Description = "Ruby Programlama Dili ile İlgili En Güncel Bilgiler",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Ruby Blog Kategorisi",
                }
            );

        }
    }
}
