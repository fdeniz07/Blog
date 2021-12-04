using CoreLayer.DataAccess.Concrete.EntityFramework;
using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete.EntityFramework.Repositories
{
    public class EfAboutRepository:EfEntityRepositoryBase<About>,IAboutRepository
    {
        //DataAccessLayer --> Concrete --> Repositories altina yukarida olusturulan entity'lere ait soyut kavramlarin somut class hallerini implemente ediyoruz. Bu class'lar DataAccessLayer --> Abstract --> icerisindeki kendine ait ilgili interface'lerden implemente edildiginde icleri doldurulmamis metotlar gelecektir. Ancak bizim daha önce icleri doldurulmus CoreLayer --> Concrete --> EntityFramework --> EfEntityRepositoryBase Class'imiz mevcuttu. Implementasyon kismina bunu da gecer ve constructor icerisinde base class'a gönderirirsek, artik bu class larimizda cok fazla kod tekraririndan kacinmis olur, DRY yapisina ve SOLID yapisina uymus oluruz.

        public EfAboutRepository(DbContext context):base(context)
        {
            
        }
    }
}
