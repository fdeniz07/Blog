using CoreLayer.DataAccess.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract.Repositories
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
    }
}
