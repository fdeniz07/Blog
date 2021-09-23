using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract.UnitOfWorks;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using DataAccessLayer.Concrete.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            // Bu yapinin amaci, kullanici bizden soyut bir nesne istediginde, biz ona otomatik olarak ilgili soyut nesneye ait bir somut nesne sunuyoruz


            serviceCollection.AddDbContext<MsDbContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IBlogService, BlogManager>();

            /*Scope
             *
             * Yapilan her request'te nesne tekrar olusur ve bir request icerisinde sadece bir tane nesne kullanilir. Bu yöntem icin de AddScope() metodu kullaniliyor.
             * Transient ve Scoped kullanim sekilleri nesne olusturma zamanlari acisindan biraz karistirilabilir. Transient'da her nesne cagrimindan yeni bir instance olusur ve o request sonlanana kadar ayni nesne kullanilir. Request bazinda stateless nesne kullanilmasi istenen durumlarda Scoped bagimliliklari olusturabiliriz.
             *
             * Kaynak : http://umutluoglu.com/2017/01/asp-net-core-dependency-injection/
             */

            return serviceCollection;
        }
    }
}
