using System.Threading.Tasks;
using BlogWeb.Models;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;
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
                await _blogService.IncreaseViewCountAsync(blogId);
                return View(blogResult.Data);
            }

            return NotFound();
        }
    }
}
