using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccessLayer.Concrete.Configurations
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).UseIdentityColumn();
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Description).HasMaxLength(250);
            builder.Property(r => r.CreatedByName).IsRequired().HasMaxLength(50);
            builder.Property(r => r.ModifiedByName).IsRequired().HasMaxLength(50);
            builder.Property(r => r.CreatedDate).IsRequired();
            builder.Property(r => r.ModifiedDate).IsRequired();
            builder.Property(r => r.IsActive).IsRequired();
            builder.Property(r => r.IsDeleted).IsRequired();
            builder.Property(r => r.Note).HasMaxLength(500);

            builder.ToTable("Roles");

            //Manuel olarak ilk örnek verimizi eklemek istersek;

            builder.HasData(
                new Role
                {
                    //DB olusturulmadan önce data olusturulacaksa mutlaka Id girilmelidir. Sonradan girilecek Id degeri girmeye gerek yoktur.
                    Id = 1,
                    Name = "Admin",
                    Description = "Admin Rolü, Tüm Haklara Sahiptir",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate", //InitialCreate degerini Db nin olusturacagini belirtiyoruz
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Admin Rolüdür."
                    //Tüm alanlar doldurulmalidir. IsRequiered olmasa bile ilk veri yazilirken kesinlikle deger girilmelidir.
                },
                new Role
                {
                    Id = 2,
                    Name = "Author",
                    Description = "Yazar Rolü, Kisitli Haklara Sahiptir",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Yazar Rolüdür."
                },
                new Role
                {
                    Id = 3,
                    Name = "User",
                    Description = "Kullanici Rolü, Kisitli Haklara Sahiptir",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Kullanici Rolüdür."
                });
        }
    }
}
