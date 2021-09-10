using CoreLayer.DataAccess.Concrete.EntityFramework;
using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete.EntityFramework.Repositories
{
    public class EfAuthorRepository : EfEntityRepositoryBase<Author>, IAuthorRepository
    {
        public EfAuthorRepository(DbContext context) : base(context)
        {

        }
    }
}
