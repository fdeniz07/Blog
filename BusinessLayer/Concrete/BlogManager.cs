using AutoMapper;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;
using CoreLayer.Utilities.Results.Concrete;
using DataAccessLayer.Abstract.UnitOfWorks;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BusinessLayer.Utilities;

namespace BusinessLayer.Concrete
{
    public class BlogManager : ManagerBase, IBlogService
    {

        public BlogManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }


        /////////////////////// GetAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<BlogDto>> GetAsync(int blogId)
        {
            var blog = await UnitOfWork.Blogs.GetAsync(b => b.Id == blogId, b => b.User, b => b.Category);
            if (blog != null)
            {
                blog.Comments = await UnitOfWork.Comments.GetAllAsync(c => c.BlogId == blogId && !c.IsDeleted && c.IsActive);
                return new DataResult<BlogDto>(ResultStatus.Success, new BlogDto
                {
                    Blog = blog,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BlogDto>(ResultStatus.Error, Messages.Blog.NotFound(isPlural: false), null);
        }


        /////////////////////// GetBlogUpdateDtoAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<BlogUpdateDto>> GetBlogUpdateDtoAsync(int blogId)
        {
            var result = await UnitOfWork.Blogs.AnyAsync(b => b.Id == blogId);
            if (result)
            {
                var blog = await UnitOfWork.Blogs.GetAsync(b => b.Id == blogId);
                var blogUpdateDto = Mapper.Map<BlogUpdateDto>(blog);
                return new DataResult<BlogUpdateDto>(ResultStatus.Success, blogUpdateDto);
            }
            else
            {
                return new DataResult<BlogUpdateDto>(ResultStatus.Error, Messages.Blog.NotFound(isPlural: false), null);
            }
        }


        /////////////////////// GetAllAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<BlogListDto>> GetAllAsync()
        {
            var blogs = await UnitOfWork.Blogs.GetAllAsync(null, b => b.User, b => b.Category);
            if (blogs.Count > -1)
            {
                return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
                {
                    Blogs = blogs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BlogListDto>(ResultStatus.Error, Messages.Blog.NotFound(isPlural: true), null);
        }


        /////////////////////// GetAllByNonDeletedAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<BlogListDto>> GetAllByNonDeletedAsync()
        {
            var blogs = await UnitOfWork.Blogs.GetAllAsync(b => !b.IsDeleted, bl => bl.User, bl => bl.Category);
            if (blogs.Count > -1)
            {
                return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
                {
                    Blogs = blogs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BlogListDto>(ResultStatus.Error, Messages.Blog.NotFound(isPlural: true), null);
        }


        /////////////////////// GetAllByNonDeletedAndActiveAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<BlogListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var blogs = await UnitOfWork.Blogs.GetAllAsync(b => !b.IsDeleted && b.IsActive, bl => bl.User,
                bl => bl.Category);
            if (blogs.Count > -1)
            {
                return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
                {
                    Blogs = blogs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BlogListDto>(ResultStatus.Error, Messages.Blog.NotFound(isPlural: true), null);
        }


        /////////////////////// GetAllByDeletedAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\ Tüm Silinmisleri Getir

        public async Task<IDataResult<BlogListDto>> GetAllByDeletedAsync()
        {
            var blogs = await UnitOfWork.Blogs.GetAllAsync(b => b.IsDeleted, bl => bl.User,
                bl => bl.Category);
            if (blogs.Count > -1)
            {
                return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
                {
                    Blogs = blogs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BlogListDto>(ResultStatus.Error, Messages.Blog.NotFound(isPlural: true), null);
        }


        /////////////////////// GetAllByCategoryAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<BlogListDto>> GetAllByCategoryAsync(int categoryId)
        {
            var result = await UnitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var blogs = await UnitOfWork.Blogs.GetAllAsync(b => b.CategoryId == categoryId && !b.IsDeleted && b.IsActive, bl => bl.User,
                    bl => bl.Category);
                if (blogs.Count > -1)
                {
                    return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
                    {
                        Blogs = blogs,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<BlogListDto>(ResultStatus.Error, Messages.Blog.NotFound(isPlural: true), null);
            }
            return new DataResult<BlogListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);
        }


        /////////////////////// GetAllByViewCountAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\   Siralama türüne ve kac tane makale almamiza göre getirecek. Mesala en cok okunan 5 makale gibi. Vermezsek hepsi gelir, verirsek istedigimiz kadar

        public async Task<IDataResult<BlogListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize)
        {
            var blogs = await UnitOfWork.Blogs.GetAllAsync(b => b.IsActive && !b.IsDeleted, b => b.User);
            var sortedBlogs =
                isAscending
                    ? blogs.OrderBy(b => b.ViewCount)
                    : blogs.OrderByDescending(b =>
                        b.ViewCount); //Orderby default olarak ascending siralama yapar (azdan coka)
            return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
            {
                Blogs = takeSize == null ? sortedBlogs.ToList() : sortedBlogs.Take(takeSize.Value).ToList() //takeSize kontrolü yapiyoruz. Vermezsek, hepsi gelecek
            });
        }


        /////////////////////// GetAllByPagingAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<BlogListDto>> GetAllByPagingAsync(int? categroyId, int currentPage = 1, int pageSize = 6, bool isAscending = false)
        {
            pageSize = pageSize > 24 ? 24 : pageSize; // Pagesize degerimizi 24 den fazla olamaz. Bu projeye göre degisiklik gösterebilir.
            var blogs = categroyId == null ? await UnitOfWork.Blogs.GetAllAsync(b => b.IsActive && !b.IsDeleted, b => b.Category, b => b.User) : await UnitOfWork.Blogs.GetAllAsync(b => b.CategoryId == categroyId && b.IsActive && !b.IsDeleted, b => b.Category, b => b.User);
            var sortedBlogs =
                isAscending
                    ? blogs.OrderBy(b => b.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList() 
                    : blogs.OrderByDescending(b => b.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(); //Skip() degeri bulunan sayfayi atla, Take() ile de sonraki sayfalardaki degerleri getir anlaminda kullaniliyor
            return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
            {
                Blogs = sortedBlogs,
                CategoryId = categroyId == null ? null : categroyId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = blogs.Count,
                IsAscending = isAscending
            });
        }


        /////////////////////// SearchAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<BlogListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 6, bool isAscending = false)
        {
            pageSize = pageSize > 24 ? 24 : pageSize; // Pagesize degerimizi 24 den fazla olamaz. Bu projeye göre degisiklik gösterebilir.
            if (string.IsNullOrWhiteSpace(keyword)) //burada kullanici arama alanina hicbirsey girmemis ya da space araciligi ile bosluklar koyarak arama yapmaya calisabilirligini kontrol ediyoruz.
            {
                var blogs = await UnitOfWork.Blogs.GetAllAsync(b => b.IsActive && !b.IsDeleted, b => b.Category, b => b.User);
                var sortedBlogs =
                    isAscending
                        ? blogs.OrderBy(b => b.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                        : blogs.OrderByDescending(b => b.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(); //Skip() degeri bulunan sayfayi atla, Take() ile de sonraki sayfalardaki degerleri getir anlaminda kullaniliyor
                return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
                {
                    Blogs = sortedBlogs,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = blogs.Count,
                    IsAscending = isAscending
                });
            }

            //Kullanici arama alanina birsey girerse, biz bunu 4 farkli kriter icerisinde arayip kullaniciya dönüyoruz.
            var searchBlogs = await UnitOfWork.Blogs.SearchAsync(new List<Expression<Func<Blog, bool>>>
            {
                (b) => b.Title.Contains(keyword),
                (b) => b.Category.CategoryName.Contains(keyword),
                (b) => b.SeoDescription.Contains(keyword),
                (b) => b.SeoTags.Contains(keyword)
            },b=>b.Category,b=>b.User);
            var searchedAndSortedBlogs =
                isAscending
                    ? searchBlogs.OrderBy(b => b.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                    : searchBlogs.OrderByDescending(b => b.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(); //Skip() degeri bulunan sayfayi atla, Take() ile de sonraki sayfalardaki degerleri getir anlaminda kullaniliyor
            return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
            {
                Blogs = searchedAndSortedBlogs,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = searchBlogs.Count,
                IsAscending = isAscending
            });
        }


        /////////////////////// AddAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> AddAsync(BlogAddDto blogAddDto, string createdByName, int userId)
        {
            var blog = Mapper.Map<Blog>(blogAddDto);
            blog.CreatedByName = createdByName;
            blog.ModifiedByName = createdByName;
            blog.UserId = userId;
            //await _unitOfWork.Blogs.AddAsync(blog).ContinueWith(t => _unitOfWork.SaveAsync()); // Bu bölüm hizli oldugu icin thread'in biri kayit ederken diger islem calisacagi icin hata aliyoruz (core 6 ile düzelebilir)
            await UnitOfWork.Blogs.AddAsync(blog);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Blog.Add(blog.Title));
        }


        /////////////////////// UpdateAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> UpdateAsync(BlogUpdateDto blogUpdateDto, string modifiedByName)
        {
            var oldBlog = await UnitOfWork.Blogs.GetAsync(b => b.Id == blogUpdateDto.Id);
            var blog = Mapper.Map<BlogUpdateDto, Blog>(blogUpdateDto, oldBlog);
            blog.ModifiedByName = modifiedByName; //Diger alanlar Automapper ile otomatik tamamlaniyor. Automapper, bize kod kalabaliginin ve zaman kaybinin önüne gecmemizi sagliyor.
            await UnitOfWork.Blogs.UpdateAsync(blog);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Blog.Update(blog.Title));
        }


        /////////////////////// DeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> DeleteAsync(int blogId, string modifiedByName)
        {
            var result = await UnitOfWork.Blogs.AnyAsync(b => b.Id == blogId);
            if (result)
            {
                var blog = await UnitOfWork.Blogs.GetAsync(b => b.Id == blogId);
                blog.IsDeleted = true;
                blog.IsActive = false;
                blog.ModifiedByName = modifiedByName;
                blog.ModifiedDate = DateTime.Now;
                await UnitOfWork.Blogs.UpdateAsync(blog);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Blog.Delete(blog.Title));
            }
            return new Result(ResultStatus.Error, Messages.Blog.NotFound(isPlural: false));
        }


        /////////////////////// UndoDeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\ Silinmisleri Geri Al
        /// 
        public async Task<IResult> UndoDeleteAsync(int blogId, string modifiedByName)
        {
            var result = await UnitOfWork.Blogs.AnyAsync(b => b.Id == blogId);
            if (result)
            {
                var blog = await UnitOfWork.Blogs.GetAsync(b => b.Id == blogId);
                blog.IsDeleted = false;
                blog.IsActive = true;
                blog.ModifiedByName = modifiedByName;
                blog.ModifiedDate = DateTime.Now;
                await UnitOfWork.Blogs.UpdateAsync(blog);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Blog.UndoDelete(blog.Title));
            }
            return new Result(ResultStatus.Error, Messages.Blog.NotFound(isPlural: false));
        }


        /////////////////////// HardDeleteAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IResult> HardDeleteAsync(int blogId)
        {
            var result = await UnitOfWork.Blogs.AnyAsync(b => b.Id == blogId);
            if (result)
            {
                var blog = await UnitOfWork.Blogs.GetAsync(b => b.Id == blogId);
                await UnitOfWork.Blogs.DeleteAsync(blog);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Blog.HardDelete(blog.Title));
            }
            return new Result(ResultStatus.Error, Messages.Blog.NotFound(isPlural: false));
        }


        /////////////////////// CountAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<int>> CountAsync()
        {
            var blogsCount = await UnitOfWork.Blogs.CountAsync();// tüm degerleri getir
            if (blogsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, blogsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }


        /////////////////////// CountByNonDeletedAsync \\\\\\\\\\\\\\\\\\\\\\\\\\\\

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var blogsCount = await UnitOfWork.Blogs.CountAsync(b => !b.IsDeleted);// Silinmemis degerleri getir
            if (blogsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, blogsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

    }
}
