using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.Configurations
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("UserRoles");

            //builder.HasData(
            //    new UserRole
            //    {
            //        RoleId = 1,
            //        UserId = 1
            //    },
            //    new UserRole
            //    {
            //        RoleId = 2,
            //        UserId = 2
            //    });

            builder.HasData(
                // Category.Create
                new UserRole
                {
                    UserId = 1,
                    RoleId = 1

                },
                // Category.Read
                new UserRole
                {
                    UserId = 1,
                    RoleId = 2

                },
                // Category.Update
                new UserRole
                {
                    UserId = 1,
                    RoleId = 3

                },
                // Category.Delete
                new UserRole
                {
                    UserId = 1,
                    RoleId = 4

                },
                // Article.Create
                new UserRole
                {
                    UserId = 1,
                    RoleId = 5

                },
                // Article.Read
                new UserRole
                {
                    UserId = 1,
                    RoleId = 6

                },
                // Article.Update
                new UserRole
                {
                    UserId = 1,
                    RoleId = 7

                },
                // Article.Delete
                new UserRole
                {
                    UserId = 1,
                    RoleId = 8

                },
                // User.Create
                new UserRole
                {
                    UserId = 1,
                    RoleId = 9

                },
                // User.Read
                new UserRole
                {
                    UserId = 1,
                    RoleId = 10
                },
                // User.Update
                new UserRole
                {
                    UserId = 1,
                    RoleId = 11

                },
                // User.Delete
                new UserRole
                {
                    UserId = 1,
                    RoleId = 12

                },
                // Role.Create
                new UserRole
                {
                    UserId = 1,
                    RoleId = 13

                },
                // Role.Read
                new UserRole
                {
                    UserId = 1,
                    RoleId = 14

                },
                // Role.Update
                new UserRole
                {
                    UserId = 1,
                    RoleId = 15

                },
                // Role.Delete
                new UserRole
                {
                    UserId = 1,
                    RoleId = 16

                },
                // Comment.Create
                new UserRole
                {
                    UserId = 1,
                    RoleId = 17

                },
                // Comment.Read
                new UserRole
                {
                    UserId = 1,
                    RoleId = 18

                },
                // Comment.Update
                new UserRole
                {
                    UserId = 1,
                    RoleId = 19

                },
                // Comment.Delete
                new UserRole
                {
                    UserId = 1,
                    RoleId = 20

                },
                // AdminArea.Home.Read
                new UserRole
                {
                    UserId = 1,
                    RoleId = 21
                },
                // SuperAdmin
                new UserRole
                {
                    UserId = 1,
                    RoleId = 22
                },
                // Category.Create
                new UserRole
                {
                    UserId = 2,
                    RoleId = 1

                },
                // Category.Read
                new UserRole
                {
                    UserId = 2,
                    RoleId = 2

                },
                // Category.Update
                new UserRole
                {
                    UserId = 2,
                    RoleId = 3

                },
                // Category.Delete
                new UserRole
                {
                    UserId = 2,
                    RoleId = 4

                },
                // Article.Create
                new UserRole
                {
                    UserId = 2,
                    RoleId = 5

                },
                // Article.Read
                new UserRole
                {
                    UserId = 2,
                    RoleId = 6

                },
                // Article.Update
                new UserRole
                {
                    UserId = 2,
                    RoleId = 7

                },
                // Article.Delete
                new UserRole
                {
                    UserId = 2,
                    RoleId = 8

                },
                // Comment.Create
                new UserRole
                {
                    UserId = 2,
                    RoleId = 17

                },
                // Comment.Read
                new UserRole
                {
                    UserId = 2,
                    RoleId = 18

                },
                // Comment.Update
                new UserRole
                {
                    UserId = 2,
                    RoleId = 19

                },
                // Comment.Delete
                new UserRole
                {
                    UserId = 2,
                    RoleId = 20

                },
                // AdminArea.Home.Read
                new UserRole
                {
                    UserId = 2,
                    RoleId = 21
                }
            );
        }
    }
}
