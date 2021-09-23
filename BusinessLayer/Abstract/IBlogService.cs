using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBlogService
    {
        Task<IDataResult<BlogDto>> Get(int blogId);
        Task<IDataResult<BlogListDto>> GetAll(); // Tüm makaleleri gösterme
        Task<IDataResult<BlogListDto>> GetAllByNonDeleted(); // Silinmemis olan makaleleri de gösterme
        Task<IDataResult<BlogListDto>> GetAllByNonDeletedAndActive(); // Hem silinmemis hem de aktif olan makaleleri getirmek icin
        Task<IDataResult<BlogListDto>> GetAllByCategory(int categoryId); //Kategoriye göre makale getirme
        Task<IResult> Add(BlogAddDto blogAddDto, string createdByName);
        Task<IResult> Update(BlogUpdateDto blogUpdateDto, string modifiedByName);
        Task<IResult> Delete(int blogId, string modifiedByName);
        Task<IResult> HardDelete(int blogId);
    }
}
