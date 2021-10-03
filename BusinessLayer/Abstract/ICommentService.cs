using CoreLayer.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<int>> Count();
        Task<IDataResult<int>> CountByNonDeleted();
    }
}
