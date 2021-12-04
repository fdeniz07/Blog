using System;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.Mapping
{
    public class BlogMap : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).UseIdentityColumn();
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.Title).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Content).IsRequired().HasColumnType("NVARCHAR(MAX)");
            builder.Property(b => b.Date).IsRequired();
            builder.Property(b => b.Image).HasMaxLength(250);
            builder.Property(b => b.Thumbnail).IsRequired().HasMaxLength(250);
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
            //        Thumbnail = "Default.jpg",
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
            //        Thumbnail = "Default.jpg",
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
            //        Thumbnail = "Default.jpg",
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


            builder.HasData(
                new Blog
                {
                    Id = 1,
                    CategoryId = 1,
                    Title = "C# 9.0 ve .NET 5 Yenilikleri",
                    Content =
                        "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
                    Thumbnail = "postImages/defaultThumbnail.jpg",
                    SeoDescription = "C# 9.0 ve .NET 5 Yenilikleri",
                    SeoTags = "C#, C# 9, .NET5, .NET Framework, .NET Core",
                    SeoAuthor = "Alper Tunga",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C# 9.0 ve .NET 5 Yenilikleri",
                    UserId = 1,
                    ViewCount = 100,
                    CommentCount = 0
                },
                new Blog
                {
                    Id = 2,
                    CategoryId = 2,
                    Title = "C++ 11 ve 19 Yenilikleri",
                    Content =
                        "Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir. Lorem Ipsum kullanmanın amacı, sürekli 'buraya metin gelecek, buraya metin gelecek' yazmaya kıyasla daha dengeli bir harf dağılımı sağlayarak okunurluğu artırmasıdır. Şu anda birçok masaüstü yayıncılık paketi ve web sayfa düzenleyicisi, varsayılan mıgır metinler olarak Lorem Ipsum kullanmaktadır. Ayrıca arama motorlarında 'lorem ipsum' anahtar sözcükleri ile arama yapıldığında henüz tasarım aşamasında olan çok sayıda site listelenir. Yıllar içinde, bazen kazara, bazen bilinçli olarak (örneğin mizah katılarak), çeşitli sürümleri geliştirilmiştir.",
                    Thumbnail = "postImages/defaultThumbnail.jpg",
                    SeoDescription = "C++ 11 ve 19 Yenilikleri",
                    SeoTags = "C++ 11 ve 19 Yenilikleri",
                    SeoAuthor = "Alper Tunga",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C++ 11 ve 19 Yenilikleri",
                    UserId = 1,
                    ViewCount = 295,
                    CommentCount = 0
                },
                new Blog
                {
                    Id = 3,
                    CategoryId = 3,
                    Title = "JavaScript ES2019 ve ES2020 Yenilikleri",
                    Content =
                        "Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia'daki Hampden-Sydney College'dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan 'consectetur' sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan \"de Finibus Bonorum et Malorum\" (İyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan \"Lorem ipsum dolor sit amet\" 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500'lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir. Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H. Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.",
                    Thumbnail = "postImages/defaultThumbnail.jpg",
                    SeoDescription = "JavaScript ES2019 ve ES2020 Yenilikleri",
                    SeoTags = "JavaScript ES2019 ve ES2020 Yenilikleri",
                    SeoAuthor = "Alper Tunga",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "JavaScript ES2019 ve ES2020 Yenilikleri",
                    UserId = 1,
                    ViewCount = 12,
                    CommentCount = 0
                },
                new Blog
                {
                    Id = 4,
                    CategoryId = 4,
                    Title = "Typescript 4.1",
                    Content =
                    $"É um facto estabelecido de que um leitor é distraído pelo conteúdo legível de uma página quando analisa a sua mancha gráfica. Logo, o uso de Lorem Ipsum leva a uma distribuição mais ou menos normal de letras, ao contrário do uso de 'Conteúdo aqui,conteúdo aqui'', tornando-o texto legível. Muitas ferramentas de publicação electrónica e editores de páginas web usam actualmente o Lorem Ipsum como o modelo de texto usado por omissão, e uma pesquisa por 'lorem ipsum' irá encontrar muitos websites ainda na sua infância. Várias versões têm evoluído ao longo dos anos, por vezes por acidente, por vezes propositadamente (como no caso do humor).",
                    Thumbnail = "postImages/defaultThumbnail.jpg",
                    SeoDescription = "Typescript 4.1, Typescript, TYPESCRIPT 2021",
                    SeoTags = "Typescript 4.1 Güncellemeleri",
                    SeoAuthor = "Alper Tunga",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Typescript 4.1 Yenilikleri",
                    UserId = 1,
                    ViewCount = 666,
                    CommentCount = 0
                },
                new Blog
                {
                    Id = 5,
                    CategoryId = 5,
                    Title = "Java ve Android'in Geleceği | 2021",
                    Content =
                        "Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia'daki Hampden-Sydney College'dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan 'consectetur' sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan \"de Finibus Bonorum et Malorum\" (İyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan \"Lorem ipsum dolor sit amet\" 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500'lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir. Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H. Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.",
                    Thumbnail = "postImages/defaultThumbnail.jpg",
                    SeoDescription = "Java, Android, Mobile, Kotlin, Uygulama Geliştirme",
                    SeoTags = "Java, Mobil, Kotlin, Android, IOS, SWIFT",
                    SeoAuthor = "Alper Tunga",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "JAVA",
                    UserId = 1,
                    ViewCount = 3225,
                    CommentCount = 0
                },
                new Blog
                {
                    Id = 6,
                    CategoryId = 6,
                    Title = "Python ile Veri Madenciliği | 2021",
                    Content =
                    $"Le Lorem Ipsum est simplement du faux texte employé dans la composition et la mise en page avant impression. Le Lorem Ipsum est le faux texte standard de l'imprimerie depuis les années 1500, quand un imprimeur anonyme assembla ensemble des morceaux de texte pour réaliser un livre spécimen de polices de texte. Il n'a pas fait que survivre cinq siècles, mais s'est aussi adapté à la bureautique informatique, sans que son contenu n'en soit modifié. Il a été popularisé dans les années 1960 grâce à la vente de feuilles Letraset contenant des passages du Lorem Ipsum, et, plus récemment, par son inclusion dans des applications de mise en page de texte, comme Aldus PageMaker.",
                    Thumbnail = "postImages/defaultThumbnail.jpg",
                    SeoDescription = "Python ile Veri Madenciliği",
                    SeoTags = "Python, Veri Madenciliği Nasıl Yapılır?",
                    SeoAuthor = "Alper Tunga",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Python",
                    UserId = 1,
                    ViewCount = 9999,
                    CommentCount = 0
                },
                new Blog
                {
                    Id = 7,
                    CategoryId = 7,
                    Title = "Php Laravel Başlangıç Rehberi | API",
                    Content =
                        $"Contrairement à une opinion répandue, le Lorem Ipsum n'est pas simplement du texte aléatoire. Il trouve ses racines dans une oeuvre de la littérature latine classique datant de 45 av. J.-C., le rendant vieux de 2000 ans. Un professeur du Hampden-Sydney College, en Virginie, s'est intéressé à un des mots latins les plus obscurs, consectetur, extrait d'un passage du Lorem Ipsum, et en étudiant tous les usages de ce mot dans la littérature classique, découvrit la source incontestable du Lorem Ipsum. Il provient en fait des sections 1.10.32 et 1.10.33 du 0De Finibus Bonorum et Malorum' (Des Suprêmes Biens et des Suprêmes Maux) de Cicéron. Cet ouvrage, très populaire pendant la Renaissance, est un traité sur la théorie de l'éthique. Les premières lignes du Lorem Ipsum, 'Lorem ipsum dolor sit amet...'', proviennent de la section 1.10.32",
                    Thumbnail = "postImages/defaultThumbnail.jpg",
                    SeoDescription = "Php ile API Oluşturma Rehberi",
                    SeoTags = "php, laravel, api, oop",
                    SeoAuthor = "Alper Tunga",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "PHP",
                    UserId = 1,
                    ViewCount = 4818,
                    CommentCount = 0
                },
                new Blog
                {
                    Id = 8,
                    CategoryId = 8,
                    Title = "Kotlin ile Mobil Programlama",
                    Content =
                        $"Plusieurs variations de Lorem Ipsum peuvent être trouvées ici ou là, mais la majeure partie d'entre elles a été altérée par l'addition d'humour ou de mots aléatoires qui ne ressemblent pas une seconde à du texte standard. Si vous voulez utiliser un passage du Lorem Ipsum, vous devez être sûr qu'il n'y a rien d'embarrassant caché dans le texte. Tous les générateurs de Lorem Ipsum sur Internet tendent à reproduire le même extrait sans fin, ce qui fait de lipsum.com le seul vrai générateur de Lorem Ipsum. Iil utilise un dictionnaire de plus de 200 mots latins, en combinaison de plusieurs structures de phrases, pour générer un Lorem Ipsum irréprochable. Le Lorem Ipsum ainsi obtenu ne contient aucune répétition, ni ne contient des mots farfelus, ou des touches d'humour.",
                    Thumbnail = "postImages/defaultThumbnail.jpg",
                    SeoDescription = "Kotlin ile Mobil Programlama Baştan Sona Adım Adım",
                    SeoTags = "kotlin, android, mobil, programlama",
                    SeoAuthor = "Alper Tunga",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Kotlin",
                    UserId = 1,
                    ViewCount = 750,
                    CommentCount = 0
                },
                new Blog
                {
                    Id = 9,
                    CategoryId = 9,
                    Title = "Swift ile IOS Programlama",
                    Content =
                        $"Al contrario di quanto si pensi, Lorem Ipsum non è semplicemente una sequenza casuale di caratteri. Risale ad un classico della letteratura latina del 45 AC, cosa che lo rende vecchio di 2000 anni. Richard McClintock, professore di latino al Hampden-Sydney College in Virginia, ha ricercato una delle più oscure parole latine, consectetur, da un passaggio del Lorem Ipsum e ha scoperto tra i vari testi in cui è citata, la fonte da cui è tratto il testo, le sezioni 1.10.32 and 1.10.33 del 'de Finibus Bonorum et Malorum' di Cicerone. Questo testo è un trattato su teorie di etica, molto popolare nel Rinascimento. La prima riga del Lorem Ipsum, 'Lorem ipsum dolor sit amet..'', è tratta da un passaggio della sezione 1.10.32.",
                    Thumbnail = "postImages/defaultThumbnail.jpg",
                    SeoDescription = "Swift ile IOS Mobil Programlama Baştan Sona Adım Adım",
                    SeoTags = "IOS, android, mobil, programlama",
                    SeoAuthor = "Alper Tunga",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Swift",
                    UserId = 1,
                    ViewCount = 14900,
                    CommentCount = 0
                },
                new Blog
                {
                    Id = 10,
                    CategoryId = 10,
                    Title = "Ruby on Rails ile AirBnb Klon Kodlayalım",
                    Content =
                        $"Esistono innumerevoli variazioni dei passaggi del Lorem Ipsum, ma la maggior parte hanno subito delle variazioni del tempo, a causa dell’inserimento di passaggi ironici, o di sequenze casuali di caratteri palesemente poco verosimili. Se si decide di utilizzare un passaggio del Lorem Ipsum, è bene essere certi che non contenga nulla di imbarazzante. In genere, i generatori di testo segnaposto disponibili su internet tendono a ripetere paragrafi predefiniti, rendendo questo il primo vero generatore automatico su intenet. Infatti utilizza un dizionario di oltre 200 vocaboli latini, combinati con un insieme di modelli di strutture di periodi, per generare passaggi di testo verosimili. Il testo così generato è sempre privo di ripetizioni, parole imbarazzanti o fuori luogo ecc.",
                    Thumbnail = "postImages/defaultThumbnail.jpg",
                    SeoDescription = "Ruby, Ruby on Rails Web Programlama, AirBnb Klon",
                    SeoTags = "Ruby on Rails, Ruby, Web Programlama, AirBnb",
                    SeoAuthor = "Alper Tunga",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "Ruby",
                    UserId = 1,
                    ViewCount = 26777,
                    CommentCount = 0
                }
                );
        }
    }
}
