using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> GetAsync(int categoryId);
        
        /// <summary>
        /// Verilen ID parametresine ait kategorinin CategoryUpdateDto temsilini geriye döner.
        /// </summary>
        /// <param name="categoryId">0'dan büyük integer bir ID degeri</param>
        /// <returns>Asenkron bir operasyon ile Task olarak islem sonucu DataResult tipinde geriye döner.</returns>
        Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId);
        
        Task<IDataResult<CategoryListDto>> GetAllAsync();
        
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync();
       
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync();
      
        Task<IDataResult<CategoryListDto>> GetAllByDeletedAsync(); //Tüm silinmis ögeleri getirme
       
        /// <summary>
        /// Verilen CategoryAddDto ve CreatedByName parametrelerine ait bilgiler ile yeni bir Category ekler
        /// </summary>
        /// <param name="categoryAddDto">categoryAddDto tipinde eklenecek kategori bilgileri</param>
        /// <param name="createdByName">string tipinde kullanicinin kullanici adi</param>
        /// <returns>Asenkron bir operasyon ile Task olarak bizlere ekleme isleminin sonucunu DataResult tipinde döner.</returns>
        Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName);
      
        Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
       
        Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName); // Silme islemi sadece IsDeleted degerini true yapar
        
        Task<IDataResult<CategoryDto>> UndoDeleteAsync(int categoryId, string modifiedByName); // IsDeleted degeri true olanlari false yapar.
        
        Task<IResult> HardDeleteAsync(int categoryId); // Degerleri Veritabanindan siler
        
        Task<IDataResult<int>> CountAsync();
        
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
