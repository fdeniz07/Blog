using System;
using System.Threading.Tasks;
using BlogWeb.Attributes;
using BlogWeb.Models;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.ComplexTypes;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlogWeb.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly BlogRightSideBarWidgetOptions _blogRightSideBarWidgetOptions;

        public BlogController(IBlogService blogService, IOptionsSnapshot<BlogRightSideBarWidgetOptions> blogRightSideBarWidgetOptions)
        {
            _blogService = blogService;
            _blogRightSideBarWidgetOptions = blogRightSideBarWidgetOptions.Value;
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
        [ViewCountFilter] //Bu attribute bizim tarafimizdan yazildi ve asagidaki Action her calistiginda buradaki filtre de calicasaktir.
        public async Task<IActionResult> Detail(int blogId)
        {
            var blogResult = await _blogService.GetAsync(blogId);
            if (blogResult.ResultStatus == ResultStatus.Success)
            {
                //var userBlogs = await _blogService.GetAllByUserIdOnFilter(blogResult.Data.Blog.UserId,
                //    FilterBy.Category, OrderBy.Date, false, 10, blogResult.Data.Blog.CategoryId, DateTime.Now,
                //    DateTime.Now, 0, 99999, 0, 99999);

                var userBlogs = await _blogService.GetAllByUserIdOnFilter(blogResult.Data.Blog.UserId,
                    _blogRightSideBarWidgetOptions.FilterBy, _blogRightSideBarWidgetOptions.OrderBy, _blogRightSideBarWidgetOptions.IsAscending, _blogRightSideBarWidgetOptions.TakeSize, _blogRightSideBarWidgetOptions.CategoryId, _blogRightSideBarWidgetOptions.StartAt,
                    _blogRightSideBarWidgetOptions.EndAt, _blogRightSideBarWidgetOptions.MinViewCount, _blogRightSideBarWidgetOptions.MaxViewCount, _blogRightSideBarWidgetOptions.MinCommentCount, _blogRightSideBarWidgetOptions.MaxCommentCount);

                //await _blogService.IncreaseViewCountAsync(blogId);
                return View(new BlogDetailViewModel
                {
                    BlogDto = blogResult.Data,
                    BlogDetailRightSideBarViewModel = new BlogDetailRightSideBarViewModel
                    {
                        BlogListDto = userBlogs.Data,
                        //Header = "Kullanıcının Aynı Kategori Üzerindeki En Çok Okunan Makaleleri",
                        Header = _blogRightSideBarWidgetOptions.Header,
                        User = blogResult.Data.Blog.User
                    }
                });
            }
            return NotFound();
        }
    }
}
