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
        Task<IDataResult<BlogListDto>> GetAllByDeletedAsync(); //Tüm silinmisleri getirme
        Task<IDataResult<BlogListDto>> GetAllByCategoryAsync(int categoryId); //Kategoriye göre makale getirme
        Task<IDataResult<BlogListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize); //Siralama türüne ve kac tane makale almamiza göre getirecek. Mesala en cok okunan 6 makale gibi. Vermezsek hepsi gelir, verirsek istedigimiz kadar
        Task<IDataResult<BlogListDto>> GetAllByPagingAsync(int? categroyId, int currentPage = 1, int pageSize = 6,
            bool isAscending = false); // Sayfalama islemleri icin kullaniliyor.
        Task<IResult> AddAsync(BlogAddDto blogAddDto, string createdByName,int userId);
        Task<IResult> UpdateAsync(BlogUpdateDto blogUpdateDto, string modifiedByName);
        Task<IResult> DeleteAsync(int blogId, string modifiedByName);
        Task<IResult> UndoDeleteAsync(int blogId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int blogId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
