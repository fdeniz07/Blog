using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBlogService
    {
        Task<IDataResult<BlogDto>> GetAsync(int blogId);
        Task<IDataResult<BlogUpdateDto>> GetBlogUpdateDtoAsync(int blogId);
        Task<IDataResult<BlogListDto>> GetAllAsync(); // Tüm makaleleri gösterme
        Task<IDataResult<BlogListDto>> GetAllByNonDeletedAsync(); // Silinmemis olan makaleleri de gösterme
        Task<IDataResult<BlogListDto>> GetAllByNonDeletedAndActiveAsync(); // Hem silinmemis hem de aktif olan makaleleri getirmek icin
        Task<IDataResult<BlogListDto>> GetAllByCategoryAsync(int categoryId); //Kategoriye göre makale getirme
        Task<IResult> AddAsync(BlogAddDto blogAddDto, string createdByName,int userId);
        Task<IResult> UpdateAsync(BlogUpdateDto blogUpdateDto, string modifiedByName);
        Task<IResult> DeleteAsync(int blogId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int blogId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
