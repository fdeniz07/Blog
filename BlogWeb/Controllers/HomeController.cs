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
using NToastNotify;

namespace BlogWeb.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger; //Loglama icin kullanildi. About kisminda deneme amacli hata verdirip, loglara yazilmasi saglandi.
        private readonly IBlogService _blogService;
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IMailService _mailService;
        private readonly IToastNotification _toastNotification;

        public HomeController(ILogger<HomeController> logger, IBlogService blogService,IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo, IMailService mailService, IToastNotification toastNotification)
        {
           // _logger = logger;
            _blogService = blogService;
            _mailService = mailService;
            _toastNotification = toastNotification;
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
            //throw new Exception("Hata!"); //Bu kisim genel hata yönetimi icin kullanildi.
            return View(_aboutUsPageInfo);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            //throw new NullReferenceException(); //Bu kisim genel hata yönetimi icin kullanildi.
            return View();
        }

        [HttpPost]
        public IActionResult Contact(EmailSendDto emailSendDto)
        {
            if (ModelState.IsValid)
            {
                var result = _mailService.SendContactEmail(emailSendDto);
                _toastNotification.AddSuccessToastMessage(result.Message,new ToastrOptions
                {
                    Title = "Başarılı İşlem"
                });
                return View();
            }
            return View(emailSendDto);
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
