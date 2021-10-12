using System.Threading.Tasks;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int blogId)
        {
            var blogResult = await _blogService.GetAsync(blogId);
            if (blogResult.ResultStatus==ResultStatus.Success)
            {
                return View(blogResult.Data);
            }

            return NotFound();
        }
    }
}
