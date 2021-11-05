using System.Threading.Tasks;
using AutoMapper;
using BlogWeb.Areas.Admin.Models;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Helpers.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NToastNotify;

namespace BlogWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OptionsController : Controller
    {
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsPageInfoWriter;
        private readonly IToastNotification _toastNotification;
        private readonly WebsiteInfo _websiteInfo;
        private readonly IWritableOptions<WebsiteInfo> _websiteInfoWriter;
        private readonly SmtpSettings _smtpSettings;
        private readonly IWritableOptions<SmtpSettings> _smtpSettingsWriter;
        private readonly BlogRightSideBarWidgetOptions _blogRightSideBarWidgetOptions;
        private readonly IWritableOptions<BlogRightSideBarWidgetOptions> _blogRightSideBarWidgetOptionsWriter;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public OptionsController(IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo, IWritableOptions<AboutUsPageInfo> aboutUsPageInfoWriter, IToastNotification toastNotification, IOptions<WebsiteInfo> websiteInfo, IWritableOptions<WebsiteInfo> websiteInfoWriter, IOptions<SmtpSettings> smtpSettings, IWritableOptions<SmtpSettings> smtpSettingsWriter, IOptionsSnapshot<BlogRightSideBarWidgetOptions> blogRightSideBarWidgetOptions, IWritableOptions<BlogRightSideBarWidgetOptions> blogRightSideBarWidgetOptionsWriter, ICategoryService categoryService, IMapper mapper)
        {
            _aboutUsPageInfoWriter = aboutUsPageInfoWriter;
            _toastNotification = toastNotification;
            _websiteInfoWriter = websiteInfoWriter;
            _smtpSettingsWriter = smtpSettingsWriter;
            _blogRightSideBarWidgetOptionsWriter = blogRightSideBarWidgetOptionsWriter;
            _categoryService = categoryService;
            _mapper = mapper;
            _blogRightSideBarWidgetOptions = blogRightSideBarWidgetOptions.Value;
            _smtpSettings = smtpSettings.Value;
            _websiteInfo = websiteInfo.Value;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
        }

        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }


        [HttpPost]
        public IActionResult About(AboutUsPageInfo aboutUsPageInfo)
        {
            if (ModelState.IsValid)
            {
                _aboutUsPageInfoWriter.Update(x =>
                {
                    x.Header = aboutUsPageInfo.Header;
                    x.Content = aboutUsPageInfo.Content;
                    x.SeoAuthor = aboutUsPageInfo.SeoAuthor;
                    x.SeoDescription = aboutUsPageInfo.SeoDescription;
                    x.SeoTags = aboutUsPageInfo.SeoTags;
                });
                _toastNotification.AddSuccessToastMessage("Hakkımızda Sayfa İçerikleri başarıyla güncellenmiştir.",new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View(aboutUsPageInfo);
            }
            return View(aboutUsPageInfo);
        }


        [HttpGet]
        public IActionResult GeneralSettings()
        {
            return View(_websiteInfo);
        }


        [HttpPost]
        public IActionResult GeneralSettings(WebsiteInfo websiteInfo)
        {
            if (ModelState.IsValid)
            {
                _websiteInfoWriter.Update(x =>
                {
                    x.Title = websiteInfo.Title;
                    x.MenuTitle = websiteInfo.MenuTitle;
                    x.SeoAuthor = websiteInfo.SeoAuthor;
                    x.SeoDescription = websiteInfo.SeoDescription;
                    x.SeoTags = websiteInfo.SeoTags;
                });
                _toastNotification.AddSuccessToastMessage("Sitenizin genel ayarları başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View(websiteInfo);
            }
            return View(websiteInfo);
        }

        [HttpGet]
        public IActionResult EmailSettings()
        {
            return View(_smtpSettings);
        }


        [HttpPost]
        public IActionResult EmailSettings(SmtpSettings smtpSettings)
        {
            if (ModelState.IsValid)
            {
                _smtpSettingsWriter.Update(x =>
                {
                    x.Server = smtpSettings.Server;
                    x.Port = smtpSettings.Port;
                    x.SenderName = smtpSettings.SenderName;
                    x.SenderEmail = smtpSettings.SenderEmail;
                    x.Username = smtpSettings.Username;
                    x.Password = smtpSettings.Password;
                });
                _toastNotification.AddSuccessToastMessage("Sitenizin e-posta ayarları başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View(smtpSettings);
            }
            return View(smtpSettings);
        }

        [HttpGet]
        public async Task<IActionResult> BlogRightSideBarWidgetSettings()
        {
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            var blogRightSideBarWidgetOptionsViewModel = _mapper.Map<BlogRightSideBarWidgetOptionsViewModel>(_blogRightSideBarWidgetOptions);
            blogRightSideBarWidgetOptionsViewModel.Categories = categoriesResult.Data.Categories;
            return View(blogRightSideBarWidgetOptionsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> BlogRightSideBarWidgetSettings(BlogRightSideBarWidgetOptionsViewModel blogRightSideBarWidgetOptionsViewModel)
        {
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            blogRightSideBarWidgetOptionsViewModel.Categories = categoriesResult.Data.Categories;

            if (ModelState.IsValid)
            {
                _blogRightSideBarWidgetOptionsWriter.Update(x =>
                {
                    x.Header = blogRightSideBarWidgetOptionsViewModel.Header;
                    x.TakeSize = blogRightSideBarWidgetOptionsViewModel.TakeSize;
                    x.CategoryId = blogRightSideBarWidgetOptionsViewModel.CategoryId;
                    x.FilterBy = blogRightSideBarWidgetOptionsViewModel.FilterBy;
                    x.OrderBy = blogRightSideBarWidgetOptionsViewModel.OrderBy;
                    x.IsAscending = blogRightSideBarWidgetOptionsViewModel.IsAscending;
                    x.StartAt = blogRightSideBarWidgetOptionsViewModel.StartAt;
                    x.EndAt = blogRightSideBarWidgetOptionsViewModel.EndAt;
                    x.MaxViewCount = blogRightSideBarWidgetOptionsViewModel.MaxViewCount;
                    x.MinViewCount = blogRightSideBarWidgetOptionsViewModel.MinViewCount;
                    x.MaxCommentCount = blogRightSideBarWidgetOptionsViewModel.MaxCommentCount;
                    x.MinCommentCount = blogRightSideBarWidgetOptionsViewModel.MinCommentCount;
                });
                _toastNotification.AddSuccessToastMessage("Makale sayfalarının widget ayarları başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View(blogRightSideBarWidgetOptionsViewModel);
            }
            return View(blogRightSideBarWidgetOptionsViewModel);
        }
    }
}
