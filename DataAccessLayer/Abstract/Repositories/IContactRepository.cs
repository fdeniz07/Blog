using CoreLayer.DataAccess.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract.Repositories
{
    public interface IContactRepository : IEntityRepository<Contact>
    {
    }
}
