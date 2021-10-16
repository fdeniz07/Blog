using System;
using System.Threading.Tasks;
using BlogWeb.Models;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 6, bool isAscending = false)
        {
            var searchResult = await _blogService.SearchAsync(keyword, currentPage, pageSize, isAscending);
            if (searchResult.ResultStatus == ResultStatus.Success)
            {
                return View(new BlogSearchViewModel
                {
                    BlogListDto = searchResult.Data,
                    Keyword = keyword
                });

            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int blogId)
        {
            var blogResult = await _blogService.GetAsync(blogId);
            if (blogResult.ResultStatus == ResultStatus.Success)
            {
                var userBlogs = await _blogService.GetAllByUserIdOnFilter(blogResult.Data.Blog.UserId,
                    FilterBy.Category, OrderBy.Date, false, 10, blogResult.Data.Blog.CategoryId, DateTime.Now,
                    DateTime.Now, 0, 99999, 0, 99999);

                await _blogService.IncreaseViewCountAsync(blogId);
                return View(new BlogDetailViewModel
                {
                    BlogDto = blogResult.Data,
                    BlogDetailRightSideBarViewModel = new BlogDetailRightSideBarViewModel
                    {
                        BlogListDto = userBlogs.Data,
                        Header = "Kullanıcının Aynı Kategori Üzerindeki En Çok Okunan Makaleleri",
                        User = blogResult.Data.Blog.User
                    }
                });
            }

            return NotFound();
        }
    }
}
