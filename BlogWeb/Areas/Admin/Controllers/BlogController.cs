using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using BlogWeb.Areas.Admin.Models;
using BlogWeb.Helpers.Abstract;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.ComplexTypes;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BlogWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;


        public BlogController(IBlogService blogService, ICategoryService categoryService, UserManager<User> userManager,IMapper mapper ,IImageHelper imageHelper):base (userManager, mapper, imageHelper)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _blogService.GetAllByNonDeletedAsync();
            if (result.ResultStatus==ResultStatus.Success) return View(result.Data);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = await _categoryService.GetAllByNonDeletedAndActiveAsync(); // silinmemis kategorileri getiriyoruz

            if (result.ResultStatus==ResultStatus.Success)
            {
                return View(new BlogAddViewModel
                {
                    Categories =result.Data.Categories
                });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add(BlogAddViewModel blogAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var blogAddDto = Mapper.Map<BlogAddDto>(blogAddViewModel);
                var imageResult = await ImageHelper.Upload(blogAddViewModel.Title, blogAddViewModel.ThumbnailFile, ImageType.Post);
                blogAddDto.Thumbnail = imageResult.Data.FullName;
                var result = await _blogService.AddAsync(blogAddDto, LoggedInUser.UserName,LoggedInUser.Id);
                if (result.ResultStatus==ResultStatus.Success)
                {
                    TempData.Add("SuccessMesage",result.Message);
                    return RedirectToAction("Index", "Blog");
                }
                else
                {
                    ModelState.AddModelError("",result.Message);
                }
            }
            var categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            blogAddViewModel.Categories = categories.Data.Categories;
            return View(blogAddViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int blogId)
        {
            var blogResult = await _blogService.GetBlogUpdateDtoAsync(blogId);
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            if (blogResult.ResultStatus==ResultStatus.Success && categoriesResult.ResultStatus==ResultStatus.Success)
            {
                var blogUpdateViewModel = Mapper.Map<BlogUpdateViewModel>(blogResult.Data);
                blogUpdateViewModel.Categories = categoriesResult.Data.Categories;
                return View(blogUpdateViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateViewModel blogUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = blogUpdateViewModel.Thumbnail;
                if (blogUpdateViewModel.ThumbnailFile!=null)
                {
                    var uploadedImageResult = await ImageHelper.Upload(blogUpdateViewModel.Title,
                        blogUpdateViewModel.ThumbnailFile, ImageType.Post);
                    blogUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus == ResultStatus.Success
                        ? uploadedImageResult.Data.FullName
                        : "postImages/defaultThumbnailImage.jpg";
                    if (oldThumbnail!= "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
                }
                var blogUpdateDto = Mapper.Map<BlogUpdateDto>(blogUpdateViewModel);
                var result = await _blogService.UpdateAsync(blogUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    if (isNewThumbnailUploaded)
                    {
                        ImageHelper.Delete(oldThumbnail);
                    }
                    TempData.Add("SuccessMessage", result.Message);
                    return RedirectToAction("Index", "Blog");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            var categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            blogUpdateViewModel.Categories = categories.Data.Categories;
            return View(blogUpdateViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int blogId)
        {
            var result = await _blogService.DeleteAsync(blogId, LoggedInUser.UserName);
            var blogResult = JsonSerializer.Serialize(result);
            return Json(blogResult);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllByNonDeletedAndActiveAsync();
            var blogResult = JsonSerializer.Serialize(blogs, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(blogResult);
        }
    }
}
