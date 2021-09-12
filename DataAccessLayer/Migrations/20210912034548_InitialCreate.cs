using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Details2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MapLocation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    About = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "VARBINARY(500)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    ThumbnailImage = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    SeoAuthor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note" },
                values: new object[,]
                {
                    { 1, "C#", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 271, DateTimeKind.Local).AddTicks(6104), "C# Programlama Dili ile Ilgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 271, DateTimeKind.Local).AddTicks(6114), "C# Blog Kategorisi" },
                    { 2, "Java", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 271, DateTimeKind.Local).AddTicks(6124), "Java Programlama Dili ile Ilgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 271, DateTimeKind.Local).AddTicks(6127), "Java Blog Kategorisi" },
                    { 3, "Java Script", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 271, DateTimeKind.Local).AddTicks(6132), "Java Script Programlama Dili ile Ilgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 271, DateTimeKind.Local).AddTicks(6135), "Java Script Blog Kategorisi" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 275, DateTimeKind.Local).AddTicks(5557), "Admin Rolü, Tüm Haklara Sahiptir", true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 275, DateTimeKind.Local).AddTicks(5568), "Admin", "Admin Rolüdür." },
                    { 2, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 275, DateTimeKind.Local).AddTicks(5578), "Yazar Rolü, Kisitli Haklara Sahiptir", true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 275, DateTimeKind.Local).AddTicks(5581), "Author", "Yazar Rolüdür." },
                    { 3, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 275, DateTimeKind.Local).AddTicks(5587), "Kullanici Rolü, Kisitli Haklara Sahiptir", true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 275, DateTimeKind.Local).AddTicks(5589), "User", "Kullanici Rolüdür." }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "Email", "FirstName", "Image", "IsActive", "IsDeleted", "LastName", "ModifiedByName", "ModifiedDate", "Note", "PasswordHash", "RoleId", "UserName" },
                values: new object[] { 1, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 284, DateTimeKind.Local).AddTicks(817), "Ilk Admin Kullanici", "fdeniz07@gmail.com", "Fatih", "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU", true, false, "Deniz", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 284, DateTimeKind.Local).AddTicks(829), "Admin Kullanicisi", new byte[] { 50, 48, 50, 99, 98, 57, 54, 50, 97, 99, 53, 57, 48, 55, 53, 98, 57, 54, 52, 98, 48, 55, 49, 53, 50, 100, 50, 51, 52, 98, 55, 48 }, 1, "fatihdeniz" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "Email", "FirstName", "Image", "IsActive", "IsDeleted", "LastName", "ModifiedByName", "ModifiedDate", "Note", "PasswordHash", "RoleId", "UserName" },
                values: new object[] { 2, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 284, DateTimeKind.Local).AddTicks(1845), "Yazar", "ahmet@gmail.com", "Ahmet", "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU", true, false, "Gündüz", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 284, DateTimeKind.Local).AddTicks(1847), "Yazar", new byte[] { 50, 48, 50, 99, 98, 57, 54, 50, 97, 99, 53, 57, 48, 55, 53, 98, 57, 54, 52, 98, 48, 55, 49, 53, 50, 100, 50, 51, 52, 98, 55, 48 }, 2, "ahmetgunduz" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "Image", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "ThumbnailImage", "Title", "UserId", "ViewsCount" },
                values: new object[] { 1, 1, 1, "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir.", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 269, DateTimeKind.Local).AddTicks(4823), new DateTime(2021, 9, 12, 5, 45, 48, 269, DateTimeKind.Local).AddTicks(4461), "Default.jpg", true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 269, DateTimeKind.Local).AddTicks(4988), "C# 9.0 ve NET 5 Yenilikleri", "Fatih Deniz", "C# 9.0 ve NET 5 Yenilikleri", "C#, C# 9, .NET 5, .NET Framework, .NET Core, .NET Core MVC", "Default.jpg", "C# 9.0 ve NET 5 Yenilikleri", 1, 100 });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "Image", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "ThumbnailImage", "Title", "UserId", "ViewsCount" },
                values: new object[] { 2, 2, 1, "Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir.", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 269, DateTimeKind.Local).AddTicks(5603), new DateTime(2021, 9, 12, 5, 45, 48, 269, DateTimeKind.Local).AddTicks(5600), "Default.jpg", true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 269, DateTimeKind.Local).AddTicks(5606), "Java ve Spring Yenilikleri", "Fatih Deniz", "Java ve Spring Yenilikleri", "Java, Spring, Spring Boot, Lombok, Eclipse, Swagger, Maven", "Default.jpg", "Java ve Spring Yenilikleri", 1, 295 });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "Image", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "ThumbnailImage", "Title", "UserId", "ViewsCount" },
                values: new object[] { 3, 3, 1, "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. ", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 269, DateTimeKind.Local).AddTicks(5614), new DateTime(2021, 9, 12, 5, 45, 48, 269, DateTimeKind.Local).AddTicks(5612), "Default.jpg", true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 269, DateTimeKind.Local).AddTicks(5617), "Java Script ES2019 ve ES2020 Yenilikleri", "Fatih Deniz", "Java Script ES2019 ve ES2020 Yenilikleri", "Java Script ES2019, Java Script ES2020", "Default.jpg", "Java Script ES2019 ve ES2020 Yenilikleri", 1, 12 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "BlogId", "Content", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note" },
                values: new object[] { 1, 1, "Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz.", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 273, DateTimeKind.Local).AddTicks(1473), true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 273, DateTimeKind.Local).AddTicks(1483), "C# Makale Yorumu" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "BlogId", "Content", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note" },
                values: new object[] { 2, 2, "Lorem Ipsum sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır.", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 273, DateTimeKind.Local).AddTicks(1493), true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 273, DateTimeKind.Local).AddTicks(1496), "Java Makale Yorumu" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "BlogId", "Content", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note" },
                values: new object[] { 3, 3, "Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir.", "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 273, DateTimeKind.Local).AddTicks(1502), true, false, "InitialCreate", new DateTime(2021, 9, 12, 5, 45, 48, 273, DateTimeKind.Local).AddTicks(1504), "Java Script Makale Yorumu" });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CategoryId",
                table: "Blogs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogId",
                table: "Comments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
