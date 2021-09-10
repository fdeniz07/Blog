using CoreLayer.DataAccess.Concrete.EntityFramework;
using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete.EntityFramework.Repositories
{
    public class EfBlogRepository : EfEntityRepositoryBase<Blog>, IBlogRepository
    {
        public EfBlogRepository(DbContext context) : base(context)
        {

        }
    }
}
