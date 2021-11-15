using System;
using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes;

namespace BusinessLayer.Abstract
{
    public interface IBlogService
    {
        Task<IDataResult<BlogDto>> GetAsync(int blogId);
        Task<IDataResult<BlogDto>> GetByIdAsync(int blogId,bool includeCategory,bool includeComments,bool includeUser);
        Task<IDataResult<BlogUpdateDto>> GetBlogUpdateDtoAsync(int blogId);
        Task<IDataResult<BlogListDto>> GetAllAsyncV2(int? categoryId, int? userId, bool? isActive, bool? isDeleted, int currentPage, int pageSize, OrderByGeneral orderBy, bool isAscending, bool includeCategory, bool includeComments, bool includeUser); //GetAllAsyncV2 metodudu tüm GetAll siniflarini kapsamakta ve daha performansli calismaktadir. Tüm sartlari icerisinde barindirmakadir.

        Task<IDataResult<BlogListDto>> GetAllAsync(); // Tüm makaleleri gösterme
        Task<IDataResult<BlogListDto>> GetAllByNonDeletedAsync(); // Silinmemis olan makaleleri de gösterme
        Task<IDataResult<BlogListDto>> GetAllByNonDeletedAndActiveAsync(); // Hem silinmemis hem de aktif olan makaleleri getirmek icin
        Task<IDataResult<BlogListDto>> GetAllByCategoryAsync(int categoryId); //Kategoriye göre makale getirme
        Task<IDataResult<BlogListDto>> GetAllByDeletedAsync(); //Tüm silinmisleri getirme
        Task<IDataResult<BlogListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize); //Siralama türüne ve kac tane makale almamiza göre getirecek. Mesala en cok okunan 6 makale gibi. Vermezsek hepsi gelir, verirsek istedigimiz kadar
        Task<IDataResult<BlogListDto>> GetAllByPagingAsync(int? categroyId, int currentPage = 1, int pageSize = 6,
            bool isAscending = false); // Sayfalama islemleri icin kullaniliyor.
        Task<IDataResult<BlogListDto>> GetAllByUserIdOnFilter(int userId, FilterBy filterBy, OrderBy orderBy,
            bool isAscending, int takeSize, int categoryId, DateTime startAt, DateTime endAt, int minViewCount,
            int maxViewCount, int minCommentCount, int maxCommentCount);
        Task<IDataResult<BlogListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 6,
            bool isAscending = false);
        Task<IResult> IncreaseViewCountAsync(int blogId);
        Task<IResult> AddAsync(BlogAddDto blogAddDto, string createdByName, int userId);
        Task<IResult> UpdateAsync(BlogUpdateDto blogUpdateDto, string modifiedByName);
        Task<IResult> DeleteAsync(int blogId, string modifiedByName);
        Task<IResult> UndoDeleteAsync(int blogId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int blogId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
