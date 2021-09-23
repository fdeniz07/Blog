﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;
using CoreLayer.Utilities.Results.Concrete;
using DataAccessLayer.Abstract.UnitOfWorks;
using EntityLayer.Concrete;
using EntityLayer.Dtos;

namespace BusinessLayer.Concrete
{
    public class BlogManager : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlogManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        #region Implementation of IBlogService

        public async Task<IDataResult<BlogDto>> Get(int blogId)
        {
            var blog = await _unitOfWork.Blogs.GetAsync(b => b.Id == blogId, b => b.User, b => b.Category);
            if (blog != null)
            {
                return new DataResult<BlogDto>(ResultStatus.Success, new BlogDto
                {
                    Blog = blog,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BlogDto>(ResultStatus.Error, "Böyle bir makale bulunamadi", null);
        }

        public async Task<IDataResult<BlogListDto>> GetAll()
        {
            var blogs = await _unitOfWork.Blogs.GetAllAsync(null, b => b.User, b => b.Category);
            if (blogs.Count > -1)
            {
                return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
                {
                    Blogs = blogs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BlogListDto>(ResultStatus.Error, "Makaleler bulunamadi", null);
        }

        public async Task<IDataResult<BlogListDto>> GetAllByNonDeleted()
        {
            var blogs = await _unitOfWork.Blogs.GetAllAsync(b => !b.IsDeleted, bl => bl.User, bl => bl.Category);
            if (blogs.Count > -1)
            {
                return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
                {
                    Blogs = blogs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BlogListDto>(ResultStatus.Error, "Makaleler bulunamadi", null);
        }

        public async Task<IDataResult<BlogListDto>> GetAllByNonDeletedAndActive()
        {
            var blogs = await _unitOfWork.Blogs.GetAllAsync(b => !b.IsDeleted && b.IsActive, bl => bl.User,
                bl => bl.Category);
            if (blogs.Count > -1)
            {
                return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
                {
                    Blogs = blogs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BlogListDto>(ResultStatus.Error, "Makaleler bulunamadi", null);
        }

        public async Task<IDataResult<BlogListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var blogs = await _unitOfWork.Blogs.GetAllAsync(b => b.CategoryId == categoryId && !b.IsActive, bl => bl.User,
                    bl => bl.Category);
                if (blogs.Count > -1)
                {
                    return new DataResult<BlogListDto>(ResultStatus.Success, new BlogListDto
                    {
                        Blogs = blogs,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<BlogListDto>(ResultStatus.Error, "Makaleler bulunamadi", null);
            }
            return new DataResult<BlogListDto>(ResultStatus.Error, "Böyle bir kategori bulunamadi", null);
        }

        public async Task<IResult> Add(BlogAddDto blogAddDto, string createdByName)
        {
            var blog = _mapper.Map<Blog>(blogAddDto);
            blog.CreatedByName = createdByName;
            blog.ModifiedByName = createdByName;
            blog.UserId = 1; //Ileride session a baglanacak
            await _unitOfWork.Blogs.AddAsync(blog).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{blogAddDto.Title} baslikli makale basariyla eklenmistir.");
        }

        public async Task<IResult> Update(BlogUpdateDto blogUpdateDto, string modifiedByName)
        {
            var blog = _mapper.Map<Blog>(blogUpdateDto);
            blog.ModifiedByName = modifiedByName; //Diger alanlar Automapper ile otomatik tamamlaniyor. Automapper, bize kod kalabaliginin ve zaman kaybinin önüne gecmemizi sagliyor.
            await _unitOfWork.Blogs.UpdateAsync(blog).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{blogUpdateDto.Title} baslikli makale basariyla güncellenmistir.");
        }

        public async Task<IResult> Delete(int blogId, string modifiedByName)
        {
            var result = await _unitOfWork.Blogs.AnyAsync(b => b.Id == blogId);
            if (result)
            {
                var blog = await _unitOfWork.Blogs.GetAsync(b => b.Id == blogId);
                blog.IsDeleted = true;
                blog.ModifiedByName = modifiedByName;
                blog.ModifiedDate=DateTime.Now;
                await _unitOfWork.Blogs.UpdateAsync(blog).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{blog.Title} baslikli makale basariyla silinmistir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir makale bulunamadi");
        }

        public async Task<IResult> HardDelete(int blogId)
        {
            var result = await _unitOfWork.Blogs.AnyAsync(b => b.Id == blogId);
            if (result)
            {
                var blog = await _unitOfWork.Blogs.GetAsync(b => b.Id == blogId);
                await _unitOfWork.Blogs.DeleteAsync(blog).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{blog.Title} baslikli makale veritabanindan basariyla silinmistir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir makale bulunamadi");
        }

        #endregion
    }
}
