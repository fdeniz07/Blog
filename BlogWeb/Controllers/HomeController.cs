using System;
using BlogWeb.Models;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.Extensions.Options;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogService _blogService;
        private readonly AboutUsPageInfo _aboutUsPageInfo;

        public HomeController(ILogger<HomeController> logger, IBlogService blogService,IOptions<AboutUsPageInfo> aboutUsPageInfo)
        {
            _logger = logger;
            _blogService = blogService;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId,int currentPage=1,int pageSize=6,bool isAscending=false)
        {
            //var blogListDto = await _blogService.GetAllByNonDeletedAndActiveAsync();
            var blogResult = await (categoryId == null
                ? _blogService.GetAllByPagingAsync(null, currentPage, pageSize,isAscending)
                : _blogService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize,isAscending));
            return View(blogResult.Data);
        }

        [HttpGet]
        public IActionResult About()
        {
            throw new Exception("Hata!");
            return View(_aboutUsPageInfo);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            throw new NullReferenceException();
            return View();
        }

        [HttpPost]
        public IActionResult Contact(EmailSendDto emailSendDto)
        {
            return View();
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
