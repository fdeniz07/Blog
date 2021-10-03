using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract.UnitOfWorks;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using DataAccessLayer.Concrete.UnitOfWorks;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection,string connectionString)
        {
            // Bu yapinin amaci, kullanici bizden soyut bir nesne istediginde, biz ona otomatik olarak ilgili soyut nesneye ait bir somut nesne sunuyoruz


            serviceCollection.AddDbContext<MsDbContext>(options => options.UseSqlServer(connectionString));
            serviceCollection.AddIdentity<User, Role>(options =>
            {
                //User Password Options
                options.Password.RequireDigit = false;//sifre de rakam olsun mu? Test asamasinda kapatiyoruz
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0; //kac adet özel karakter türü olsun?
                options.Password.RequireNonAlphanumeric = false; //aktif oldugunda özel karakterlerin kullanilmasini saglar
                options.Password.RequireLowercase = false; //kücük harf kullanilmasi zorunlulugu olsun mu?
                options.Password.RequireUppercase = false; //büyük harf kullanilmasi zorunlulugu olsun mu?

                //User Username and Email Options
                options.User.AllowedUserNameCharacters= "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$"; //Kullanici adinda hangi karakterler olsun
                options.User.RequireUniqueEmail = true; // ayni mail adresi ile kayda izin verilmemeli mi?
            }).AddEntityFrameworkStores<MsDbContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IBlogService, BlogManager>();


            #region Scope
            /*
             * Yapilan her request'te nesne tekrar olusur ve bir request icerisinde sadece bir tane nesne kullanilir. Bu yöntem icin de AddScope() metodu kullaniliyor.
             * Transient ve Scoped kullanim sekilleri nesne olusturma zamanlari acisindan biraz karistirilabilir. Transient'da her nesne cagrimindan yeni bir instance olusur ve o request sonlanana kadar ayni nesne kullanilir. Request bazinda stateless nesne kullanilmasi istenen durumlarda Scoped bagimliliklari olusturabiliriz.
             *
             * Kaynak : http://umutluoglu.com/2017/01/asp-net-core-dependency-injection/
             */
            #endregion


            return serviceCollection;
        }
    }
}
