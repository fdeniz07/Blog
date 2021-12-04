using System;
using System.Threading.Tasks;
using DataAccessLayer.Abstract.Repositories;

namespace DataAccessLayer.Abstract.UnitOfWorks
{
    public interface IUnitOfWork:IAsyncDisposable
    {

        //Bu pattern'in özelligi: Coklu islem yapildiginda ve islem adimlarinin birinde hata oldugunda DB tarafindaki Rollback islemlerinin yapilmasina gerek duymamamizi
        //saglayan, yapilan degisiklikleri gecici hafizada tutan ve commit isleminden sonra hata olmazsa, verilen db ye kaydedilmesini saglar.

        //IAsyncDisposable interface'ini kullanma nedenimiz garbage collector (cöp toplayici) ya yardimci olacagiz.

        //Tüm Repository'lerimizi bir property olarak ekliyoruz.
        IAboutRepository Abouts { get; }
        IAuthorRepository Authors { get; }  //unitofwork.Authors olarak cagirabiliriz
        IBlogRepository Blogs { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IContactRepository Contacts { get; }//_unitOfWork.Contacts.AddAsync(); olarak kullanabiliriz
        IUserRepository Users { get; }


        /*Coklu kayit islemini asagidaki gibi yapiyoruz bu pattern ile.
         *
         * Eger kayit sirasinda bir islem adiminda hata olursa, hata mesaji firlatacaktir.
         *
         * Task<int> olarak tutmamizin nedeni ise gerceklesen kayit sayisina ihtiyac duyabiliriz
        */

        //_unitOfWork.Students.AddAsync(author);
        //_unitOfWork.Books.AddAsync(blog);
        //_unitOfWork.SaveAsync();

        //Bizler EfEntityRepositoryBase sinifi icerisinde hicbir save metodu tanimlamadik. Onun yerine burada tanimliyoruz.
        Task<int> SaveAsync(); // asenkron kayit yapmak istersek bu metodu kullanabiliriz.

        void Save(); // senkron kayit yapmak istersek bu metodu kullanabiliriz.

    }
}
