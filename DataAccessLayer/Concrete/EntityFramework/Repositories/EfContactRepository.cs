using CoreLayer.DataAccess.Concrete.EntityFramework;
using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete.EntityFramework.Repositories
{
    public class EfContactRepository : EfEntityRepositoryBase<Contact>, IContactRepository
    {
        public EfContactRepository(DbContext context) : base(context)
        {

        }
    }
}
