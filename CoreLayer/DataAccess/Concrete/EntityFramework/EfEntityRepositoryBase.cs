using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreLayer.DataAccess.Abstract;
using CoreLayer.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity:class,IEntity,new() //referans tip olmali, IEntity den türetilmeli ve instance alinabilmeli
    {
        protected readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            //await _context.Set<TEntity>().AddAsync(entity);
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicate != null) //eger sorgu bos degilse istedigimiz sorgular calissin
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any()) //bu dizinin icerisinde bir deger varsa, icerisinde döngü ile dönecegiz
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync(); //yukarida dönen degerleri kullanicaya bir liste olarak dönecegiz.
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.SingleOrDefaultAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);

            //Remove / Delete islemleri asenkron islemler degildir. Eger asenkron yapilmak istenirse, bizim tarafimizca yeni bir Task olusturulup, icerisine anonim bir metot yazilmalidir.
            // await Task.Run(() => { _dbSet.Remove(entity); }); --> olmalidir.
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified; // Burasi(EntityState.Modified;) cok sütunlü tablolarda kullanmakta cok kullanisli olur.
                                                                 // Tek dezavantaji, bir alan bile degisse tüm entity alanlarini güncellemeye calisir

            //entity.name = product.name
            //entity.price = product.price ile yukaridaki performans sorunu azaltilabilir ama cok satira sahip tablolarda ölümcül olabilir.

            //Update islemleri asenkron islemler degildir. Eger asenkron yapilmak istenirse, bizim tarafimizca yeni bir Task olusturulup, icerisine anonim bir metot yazilmalidir.
            // await Task.Run(() => { _dbSet.Updatet(entity); }); --> olmalidir.

            return entity;
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
