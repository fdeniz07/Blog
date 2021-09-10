using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreLayer.Entities.Abstract;

namespace CoreLayer.DataAccess.Abstract
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new() //where sartlari ile sadece veritabani nesnelerimizin buraya gelecegini belirtiyoruz
    {
        //Buraya yazdigimiz metotlar, tüm entitlerde ortak kullanmak istedigimiz metodlardir.

        //Biz burada cok dinamik bir yapi kuruyoruz. Kullanicinin bilgilerini giriyoruz(filtreden gelen deger),
        //kullanicinin diger bilgilerini cagirmak icinde includProperties kullaniliyoruz, params anahtari ile birden fazla includeproperties getirtip dizeye atiyoruz 
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetByIdAsync(int id);

        //Biz burada tüm makaleleri(null) de görmek isteyebiliriz ya da bir kategorideki bir makaleyi(filtreye göre) de görmek isteyebiliriz
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate); // predicate = Lambda

        // category.SingleOrDefaultAsync(x=>x.name = "teknoloji")
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity); //Tekil ekleme

        Task AddRangeAsync(IEnumerable<TEntity> entities);  //Coklu Ekleme

        //Silme islemi EntityFramework de senkron olarak yapilir.
        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        //var result = _userRepository.AnyAsync(u=>u.UserName == "Admin"); --> Admin isimli bir kullanici var mi
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate); //Böyle bir entity daha önceden var mi diye kontrol ediyoruz 

        //Tüm entity lerin sayisini dönmek icin de Count kullaniyoruz (var commentCount = _commentRepository.CountAsync())
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
