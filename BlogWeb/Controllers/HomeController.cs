using BlogWeb.Models;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogService _blogService;

        public HomeController(ILogger<HomeController> logger, IBlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId)
        {
            //var blogListDto = await _blogService.GetAllByNonDeletedAndActiveAsync();
            var blogResult = await (categoryId == null
                ? _blogService.GetAllByNonDeletedAndActiveAsync()
                : _blogService.GetAllByCategoryAsync(categoryId.Value));
            return View(blogResult.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
