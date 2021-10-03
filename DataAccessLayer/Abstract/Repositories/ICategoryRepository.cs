using System.Threading.Tasks;
using CoreLayer.DataAccess.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract.Repositories
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
        Task<Category> GetById(int categoryId);
    }
}
