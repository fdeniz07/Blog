using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;
using CoreLayer.Utilities.Results.Concrete;
using DataAccessLayer.Abstract.UnitOfWorks;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using System;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ManagerBase, ICategoryService
    {



        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }



        /////////////////////// GetAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            //Queryable ne is görüyor
            /*
             * Farzedelim, GetAsync icerisinde kategoriyi alirken,onun makalelerini include etmek istiyoruz ve makale icerisindeki yorumlari da onun sonrasinda ThenInclude ile include etmek istiyoruz. Burada komplex bir sorgu ile karsilasiyoruz. Bunu normal metotlarimiz icerisinde tamamlayamiyoruz. O zaman,
             * Bir queryable bir nesne olusturur
             * include ve ThenInclude icerisinde islemimizi yapabiliriz.
             *
             * Bu sayede GenericRepository nin bize yardimci olamadigi kisimlarda, queryable nesnemizi kullanabiliriz. Bu nesneyi, bu projemizdeki core katmani altinda yeralan IEntityRepository ve EfEntityRepositoryBase bölümlerde tanimlayip,implemente ediyoruz.
             */
            var query = UnitOfWork.Categories.GetAsQueryable();
            query.Include(c => c.Blogs).ThenInclude(b => b.Comments);


            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Category.NotFound(isPlural: false)
            });
        }



        /////////////////////// GetCategoryUpdateDtoAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        /// <summary>
        /// Verilen ID parametresine ait kategorinin CategoryUpdateDto temsilini geriye döner.
        /// </summary>
        /// <param name="categoryId">0'dan büyük integer bir ID degeri</param>
        /// <returns>Asenkron bir operasyon ile Task olarak islem sonucu DataResult tipinde geriye döner.</returns>
        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId)
        {
            var result = await UnitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = Mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            else
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);
            }
        }


        /////////////////////// GetAllAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(null);

            if (categories.Count > -1) //Hic kategorisi de olmayabilir. O yüzden 0 yerine -1 yaziyoruz
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Category.NotFound(isPlural: true)
            });
        }


        /////////////////////// GetAllByNonDeletedAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync() //Silinmemis olanlari getir
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(c => !c.IsDeleted);

            if (categories.Count > -1) //Hic kategorisi de olmayabilir. O yüzden 0 yerine -1 yaziyoruz
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Category.NotFound(isPlural: true)
            });
        }


        /////////////////////// GetAllByNonDeletedAndActiveAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive);

            if (categories.Count > -1) //Hic kategorisi de olmayabilir. O yüzden 0 yerine -1 yaziyoruz
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), null);
        }


        /////////////////////// GetAllByDeletedAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\ Silinmisleri Getir

        public async Task<IDataResult<CategoryListDto>> GetAllByDeletedAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(c => c.IsDeleted);

            if (categories.Count > -1) //Hic kategorisi de olmayabilir. O yüzden 0 yerine -1 yaziyoruz
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), null);
        }


        /////////////////////// AddAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        /// <summary>
        /// Verilen CategoryAddDto ve CreatedByName parametrelerine ait bilgiler ile yeni bir Category ekler
        /// </summary>
        /// <param name="categoryAddDto">categoryAddDto tipinde eklenecek kategori bilgileri</param>
        /// <param name="createdByName">string tipinde kullanicinin kullanici adi</param>
        /// <returns>Asenkron bir operasyon ile Task olarak bizlere ekleme isleminin sonucunu DataResult tipinde döner.</returns>
        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName)
        {
            #region Before using Automapper
            //await _unitOfWork.Categories.AddAsync(new Category
            //{
            //    CategoryName = categoryAddDto.CategoryName,
            //    Description = categoryAddDto.Description,
            //    Note = categoryAddDto.Note,
            //    IsActive = categoryAddDto.IsActive,
            //    CreatedByName = createdByName,
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = createdByName,
            //    ModifiedDate = DateTime.Now,
            //    IsDeleted = false
            //}).ContinueWith(t => _unitOfWork.SaveAsync());
            //ContinueWith den itibaren zincirleme task islemini devam ettiriyoruz. Kayit daha tamamlanmadan asagidaki result kismina gecer. Cok performansli bir yapi olmasina karsi yönetimi biraz daha zor bir yapidir.
            // await _unitOfWork.SaveAsync();
            #endregion

            //After using Automapper

            var category = Mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await UnitOfWork.Categories.AddAsync(category);
            await UnitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Success,
                Messages.Category.Add(addedCategory.CategoryName), new CategoryDto
                {
                    Category = addedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Category.Add(addedCategory.CategoryName)
                });

        }


        /////////////////////// UpdateAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            #region Before using Automapper
            //var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            //if (category!=null)
            //{
            //    category.CategoryName = categoryUpdateDto.CategoryName;
            //    category.Description = categoryUpdateDto.Description;
            //    category.Note = categoryUpdateDto.Note;
            //    category.IsActive = categoryUpdateDto.IsActive;
            //    category.IsDeleted = categoryUpdateDto.IsDeleted;
            //    category.ModifiedByName = modifiedByName;
            //    category.ModifiedDate = DateTime.Now;
            //    await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
            //    return new Result(ResultStatus.Success, $"{categoryUpdateDto.CategoryName} adli kategori basariyla güncellenmistir.");
            //}
            #endregion

            //After using Automapper
            var oldCategory = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var category = Mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategory);
            category.ModifiedByName = modifiedByName;

            var updatedCategory = await UnitOfWork.Categories.UpdateAsync(category);
            await UnitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.Update(updatedCategory.CategoryName), new CategoryDto
            {
                Category = updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Category.Update(updatedCategory.CategoryName)
            });
        }


        /////////////////////// DeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName)
        {
            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                category.IsDeleted = true;
                category.IsActive = false;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;

                var deletedCategory = await UnitOfWork.Categories.UpdateAsync(category);
                await UnitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.Delete(deletedCategory.CategoryName), new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Category.Delete(deletedCategory.CategoryName)
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Category.NotFound(isPlural: false)
            });
        }


        /////////////////////// UndoDeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<CategoryDto>> UndoDeleteAsync(int categoryId, string modifiedByName)
        {
            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                category.IsDeleted = false;
                category.IsActive = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;

                var deletedCategory = await UnitOfWork.Categories.UpdateAsync(category);
                await UnitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.UndoDelete(deletedCategory.CategoryName), new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Category.UndoDelete(deletedCategory.CategoryName)
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Category.NotFound(isPlural: false)
            });
        }

        /////////////////////// HardDeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> HardDeleteAsync(int categoryId)
        {
            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                await UnitOfWork.Categories.DeleteAsync(category);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Category.HardDelete(category.CategoryName));
            }
            return new Result(ResultStatus.Error, Messages.Category.NotFound(isPlural: false));
        }


        /////////////////////// CountAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<int>> CountAsync()
        {
            var categoriesCount = await UnitOfWork.Categories.CountAsync();// tüm degerleri getir
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }


        /////////////////////// CountByNonDeletedAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var categoriesCount = await UnitOfWork.Categories.CountAsync(c => !c.IsDeleted);// Silinmemis degerleri getir
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }
    }
}
