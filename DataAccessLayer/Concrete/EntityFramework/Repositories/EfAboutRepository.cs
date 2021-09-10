using CoreLayer.DataAccess.Concrete.EntityFramework;
using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete.EntityFramework.Repositories
{
    public class EfAboutRepository:EfEntityRepositoryBase<About>,IAboutRepository
    {
        public EfAboutRepository(DbContext context):base(context)
        {
            
        }
    }
}
