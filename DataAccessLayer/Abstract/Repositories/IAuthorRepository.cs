using CoreLayer.DataAccess.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract.Repositories
{
    public interface IAuthorRepository:IEntityRepository<Author>
    {
        //IEntityRepository interface'inden implemente edilmis IEntityNameRepository seklinde interface'ler olusturulur.Ortak olan tüm metodlar otomatik olarak interface den implemente edilmektedir.Eger bizler ilgili Entity'e özgü bir metot yazmak istersek burada yazacagiz. Simdilik ortak metotlar isimizi görecektir.
    }
}
