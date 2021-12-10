using CoreLayer.Utilities.Results.Abstract;
using System.Threading.Tasks;
using EntityLayer.Dtos;

namespace BusinessLayer.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<CommentDto>> GetAsync(int commentId);
       
        Task<IDataResult<CommentUpdateDto>> GetCommentUpdateDtoAsync(int commentId);
        
        Task<IDataResult<CommentListDto>> GetAllAsync();
        
        Task<IDataResult<CommentListDto>> GetAllByDeletedAsync();
        
        Task<IDataResult<CommentListDto>> GetAllByNonDeletedAsync();
        
        Task<IDataResult<CommentListDto>> GetAllByNonDeletedAndActiveAsync();
        
        Task<IDataResult<CommentDto>> ApproveAsync(int commentId,string modifiedByName); //Yorumu Onaylama
        
        Task<IDataResult<CommentDto>> AddAsync(CommentAddDto commentAddDto);
        
        Task<IDataResult<CommentDto>> UpdateAsync(CommentUpdateDto commentUpdateDto, string modifiedByName);
        
        Task<IDataResult<CommentDto>> DeleteAsync(int commentId, string modifiedByName);
        
        Task<IDataResult<CommentDto>> UndoDeleteAsync(int commentId, string modifiedByName);
        
        Task<IResult> HardDeleteAsync(int commentId);
        
        Task<IDataResult<int>> CountAsync();
        
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
