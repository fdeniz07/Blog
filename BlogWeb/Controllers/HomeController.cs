using AutoMapper;
using BlogWeb.Areas.Admin.Models;
using BlogWeb.Helpers.Abstract;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Extensions;
using CoreLayer.Utilities.Helpers.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.ComplexTypes;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Text.Json;
using System.Threading.Tasks;
using BlogWeb.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlogWeb.Controllers
{
    [Route("/")] //Herhangi bir controller üzerine route eklenirse, controller üzerindeki tüm action lara route bilgisinin gecilmesi gerekir.
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger; //Loglama icin kullanildi. About kisminda deneme amacli hata verdirip, loglara yazilmasi saglandi.
        private readonly IBlogService _blogService;
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IMailService _mailService;
        private readonly IToastNotification _toastNotification;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsPageInfoWriter;

        public HomeController(/*ILogger<HomeController> logger,*/ IBlogService blogService, IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo, IMailService mailService, IToastNotification toastNotification, IWritableOptions<AboutUsPageInfo> aboutUsPageInfoWriter)
        {
            // _logger = logger;
            _blogService = blogService;
            _mailService = mailService;
            _toastNotification = toastNotification;
            _aboutUsPageInfoWriter = aboutUsPageInfoWriter;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
        }

        [Route("index")]
        [Route("anasayfa")]
        [Route("")] //Ilgili controller ile ilgili default bir action varsa "" bos olarak birakilir
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId,int currentPage=1,int pageSize=6,bool isAscending=false)
        {
            //var blogListDto = await _blogService.GetAllByNonDeletedAndActiveAsync();
            var blogResult = await (categoryId == null
                ? _blogService.GetAllByPagingAsync(null, currentPage, pageSize,isAscending)
                : _blogService.GetAllByPagingAsync(categoryId.Value, currentPage, pageSize,isAscending));
            return View(blogResult.Data);
        }

        [Route("hakkimizda")] // sitemiz türkce oldugu icin türkce seo isimlendirme tekniklerini uyguluyoruz
        [Route("hakkinda")]
        //[Route("about")]
        [HttpGet]
        public IActionResult About()
        {
            //throw new Exception("Hata!"); //Bu kisim genel hata yönetimi icin kullanildi.
            return View(_aboutUsPageInfo);
        }

        [Route("iletisim")]
        //[Route("contact")]
        [HttpGet]
        public IActionResult Contact()
        {
            //throw new NullReferenceException(); //Bu kisim genel hata yönetimi icin kullanildi.
            return View();
        }

        [Route("iletisim")]
        //[Route("contact")]
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


        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

    }
}
