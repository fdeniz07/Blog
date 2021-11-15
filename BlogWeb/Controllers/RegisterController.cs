using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using BlogWeb.Areas.Admin.Controllers;
using BlogWeb.Areas.Admin.Models;
using BlogWeb.Helpers.Abstract;
using BlogWeb.Helpers.Concrete;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Extensions;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.ComplexTypes;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Identity;
using NToastNotify;

namespace BlogWeb.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IToastNotification _toastNotification;

        public RegisterController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, IImageHelper imageHelper, IToastNotification toastNotification) : base(userManager, mapper, imageHelper)
        {
            _toastNotification = toastNotification;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserAddDto userAddDto)
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
                var uploadedImageDtoResult = await ImageHelper.Upload(userAddDto.UserName, userAddDto.ImageFile, ImageType.User);
                userAddDto.Image = uploadedImageDtoResult.ResultStatus == ResultStatus.Success
                    ? uploadedImageDtoResult.Data.FullName : "userImages/defaultUser.png";
                var user = Mapper.Map<User>(userAddDto);
                var result = await UserManager.CreateAsync(user, userAddDto.Password); //burada bize IdentityResult dönüyor
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
    }
}
