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

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural:false), new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Category.NotFound(isPlural: false)
            });
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            else
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null);

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

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted() //Silinmemis olanlari getir
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted);

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

            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive);

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

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
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

            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Success,
                Messages.Category.Add(addedCategory.CategoryName), new CategoryDto
                {
                    Category = addedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Category.Add(addedCategory.CategoryName)
                });

        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
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
            var oldCategory = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var category = _mapper.Map<CategoryUpdateDto,Category>(categoryUpdateDto,oldCategory);
            category.ModifiedByName = modifiedByName;

            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.Update(updatedCategory.CategoryName), new CategoryDto
            {
                Category = updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Category.Update(updatedCategory.CategoryName)
            });
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;

                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
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

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Category.HardDelete(category.CategoryName));
            }
            return new Result(ResultStatus.Error, Messages.Category.NotFound(isPlural: false));
        }

        public async Task<IDataResult<int>> Count()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync();// tüm degerleri getir
            if (categoriesCount>-1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeleted()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync(c=>!c.IsDeleted);// Silinmemis degerleri getir
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
