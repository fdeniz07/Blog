using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using BlogWeb.Areas.Admin.Models;
using CoreLayer.Utilities.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace BlogWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            #region ÖZET
            /*
             * Burada bize bir UserAddDto geliyor
             *
             * elimize gercekten resim ve diger bilgiler gelmisse (ModelState.IsValid),
             * o zaman biz bu resmi vererek asagidan upload isleminin gerceklesmesini ve upload islemi sonucunda ortaya cikan resmin adini da userAddDto icerisindeki resim alanina atamis olduk.
             *
             *Daha sonrasinda UserAddDto kullanarak AutoMapper ile bir User nesnesi olusturduk ve onun sonucunda UserManager'in CreateAsync metodu ile öncelikle user nesnesimizi, daha sonrasinda da kullanicinin sifresini(user, userAddDto.Password) ham haliyle parametre olarak verdik. Bunun sonucunda IdentityResult döndü.
             *
             *IdentityResult i (kullanici basariyla eklendi mi eklenmedi mi olarak) kontrol ettik.
             * Bunun sonucunda userAddAjaxModel olusturduk, bu model icerisinde userAddDto ve UserAddPartial bilgisi bulunuyor. Bunlari Json türünde formatladiktan sonra Frontend tarafina return ettik.
             * Eger burada identity tarafindan bir hata alabilecegimizi düsünerekten onu da kontrol islemine tabi tuttuk.
             *
             * O yüzden onu da kontrol etmek acisindan bir else kismi ekledik. bu else kismi icerisinde identityResult kismi icerisinde bulunan result.Errors kismini foreach ile döndük. Herbir hata icin ModelState icerisine bir tane Error degeri ekledik
             * Bu error icerisinde de bu hatalarin aciklamasi AddModelError("", error.Description) mevcut. Bu hatalar simdilik ingilizce. Sonrasinda Türkceye cevrilecek.
             *
             * Bu hatalari ve view i frontend tarafina dönebilmek icin tekrardan UserAddAjaxViewModel olusturduk. Bunun icerisinde UserAddDto yu verdik, UserAddPartial geriye döndük.
             *
             * Bu islemlerden sonra bir islem daha düsündük,
             * Olur da ModelState durumu Invalid olarak geriye dönerse;
             * UserAddDto geri dönüyor ve bir view i icerisindeki hatalarla beraber return ediyoruz. Bu islem sonucunda isvalid degilse kullanici hatalari da görmüs oluyor.
             */
            #endregion

            if (ModelState.IsValid)
            {
                userAddDto.Image = await ImageUpload(userAddDto);
                var user = _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password); //burada bize IdentityResult dönüyor
                if (result.Succeeded) //IdentityResult basarili ise
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{user.UserName} adlı kullanıcı adına sahip, kullanıcı başarıyla eklenmiştir.",
                            User = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserAddDto = userAddDto,
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxErrorModel);
                }
            }
            var userAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
            });
            return Json(userAddAjaxModelStateErrorModel);
        }

        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            // ~/img/user.Picture
            string wwwroot = _env.WebRootPath;

            //string fileName2 = Path.GetFileNameWithoutExtension(userAddDto.ImageFile.FileName); // fatihdeniz
            string fileExtension = Path.GetExtension(userAddDto.ImageFile.FileName); //.png
            DateTime dateTime = DateTime.Now;

            //FatihDeniz_601_5_38_12_28_09_2021_userFatihDenizResmi.png
            //string fileName = $"{userAddDto.UserName}_{dateTime.FullDateAndTimeStringWithUnderscore()}_{fileName2}";

            //FatihDeniz_601_5_38_12_28_09_2021.png
            string fileName = $"{userAddDto.UserName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await userAddDto.ImageFile.CopyToAsync(stream);
            }

            return fileName; // FatihDeniz_587_5_38_12_28_09_2021.png - "~/img/user.Image"
        }
    }
}
