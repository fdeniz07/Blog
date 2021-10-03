using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;

namespace BlogWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _blogService.GetAllByNonDeletedAsync();
            if (result.ResultStatus==ResultStatus.Success) return View(result.Data);
            return NotFound();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
