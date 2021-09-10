using CoreLayer.DataAccess.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract.Repositories
{
    public interface ICommentRepository : IEntityRepository<Comment>
    {
    }
}
