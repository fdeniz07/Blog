using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Text;

namespace DataAccessLayer.Concrete.Configurations
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).UseIdentityColumn();
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(50);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(20);
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired().HasColumnType("VARBINARY(500)");
            builder.Property(u => u.Description).HasMaxLength(500);
            builder.Property(u => u.Image).IsRequired().HasMaxLength(250);
            builder.Property(u => u.CreatedByName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.ModifiedByName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.CreatedDate).IsRequired();
            builder.Property(u => u.ModifiedDate).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.Property(u => u.Note).HasMaxLength(500);

            //Bir role ait birden fazla kullanici olabilir
            builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);

            builder.ToTable("Users");

            //Manuel olarak ilk örnek verimizi eklemek istersek;

            builder.HasData(
                new User
                {
                    //DB olusturulmadan önce data olusturulacaksa mutlaka Id girilmelidir. Sonradan girilecek Id degeri girmeye gerek yoktur.
                    Id = 1,
                    RoleId = 1,
                    FirstName = "Fatih",
                    LastName = "Deniz",
                    UserName = "fatihdeniz",
                    Description = "Ilk Admin Kullanici",
                    Email = "fdeniz07@gmail.com",
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate", //InitialCreate degerini Db nin olusturacagini belirtiyoruz
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Admin Kullanicisi",
                    PasswordHash = Encoding.ASCII.GetBytes("202cb962ac59075b964b07152d234b70") //123
                   //Tüm alanlar doldurulmalidir. IsRequiered olmasa bile ilk veri yazilirken kesinlikle deger girilmelidir.
                },
                new User
                {
                    Id = 2,
                    RoleId = 2,
                    FirstName = "Ahmet",
                    LastName = "Gündüz",
                    UserName = "ahmetgunduz",
                    Description = "Yazar",
                    Email = "ahmet@gmail.com",
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Yazar",
                    PasswordHash = Encoding.ASCII.GetBytes("202cb962ac59075b964b07152d234b70") //123
                });
        }
    }
}
