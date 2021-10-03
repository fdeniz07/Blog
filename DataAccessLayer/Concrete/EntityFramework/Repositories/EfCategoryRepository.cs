using CoreLayer.DataAccess.Concrete.EntityFramework;
using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace DataAccessLayer.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository:EfEntityRepositoryBase<Category>,ICategoryRepository
    {
        public EfCategoryRepository(DbContext context):base(context)
        {
            
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await MsDbContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        }


        //EfEntityRepositoryBase disinda kendi islemlerimizi tanimlayabilmek icin burada projede kullandigimizi context'i cast islemine tabi tutuyoruz
        private MsDbContext MsDbContext
        {
            get
            {
                return _context as MsDbContext;
            }
        }
    }
}
