using AutoMapper;
using BlogWeb.Areas.Admin.Models;
using CoreLayer.Utilities.Extensions;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlogWeb.Helpers.Abstract;
using EntityLayer.ComplexTypes;
using NToastNotify;

namespace BlogWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IToastNotification _toastNotification;

        public UserController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, IImageHelper imageHelper, IToastNotification toastNotification):base(userManager, mapper, imageHelper)
        {
            _signInManager = signInManager;
            _toastNotification = toastNotification;
        }

        [Authorize(Roles = "SuperAdmin,User.Read")]
        public async Task<IActionResult> Index()
        {
            var users = await UserManager.Users.ToListAsync();
            //ImageDelete("test");
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }

        [Authorize(Roles = "SuperAdmin,User.Read")]
        [HttpGet]
        public async Task<PartialViewResult> GetDetail(int userId)
        {
            var user = await UserManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return PartialView("_GetDetailPartial", new UserDto { User = user });
        }

        [Authorize(Roles = "SuperAdmin,User.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await UserManager.Users.ToListAsync();
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

        [Authorize(Roles = "SuperAdmin,User.Create")]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }

        [Authorize(Roles = "SuperAdmin,User.Create")]
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
               var uploadedImageDtoResult = await ImageHelper.Upload(userAddDto.UserName, userAddDto.ImageFile,ImageType.User);
               userAddDto.Image = uploadedImageDtoResult.ResultStatus == ResultStatus.Success
                   ? uploadedImageDtoResult.Data.FullName: "userImages/defaultUser.png";
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


        [Authorize(Roles = "SuperAdmin,User.Delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await UserManager.FindByIdAsync(userId.ToString());
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı başarıyla silinmiştir.",
                    User = user
                });
                return Json(deletedUser);
            }
            else
            {
                string errorMessages = String.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessages = $"*{error.Description}\n";
                }

                var deletedUserErrorModel = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı silinirken bazı hatalar oluştu.\n{errorMessages}",
                    User = user
                });
                return Json(deletedUserErrorModel);
            }
        }

        [Authorize(Roles = "SuperAdmin,User.Update")]
        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await UserManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = Mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }

        [Authorize(Roles = "SuperAdmin,User.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewImageUploaded = false;
                var oldUser = await UserManager.FindByIdAsync(userUpdateDto.Id.ToString()); //kullaniciyi güncellemeden bilgilerini burada sakliyoruz
                var oldUserImage = oldUser.Image; //kullanicinin eski resmini bir degiskene atiyoruz
                if (userUpdateDto.ImageFile != null) //Eger kullanici yeni bir resim yüklerse
                {
                    var uploadedImageDtoResult = await ImageHelper.Upload(userUpdateDto.UserName, userUpdateDto.ImageFile,ImageType.User);
                    userUpdateDto.Image = uploadedImageDtoResult.ResultStatus == ResultStatus.Success
                        ? uploadedImageDtoResult.Data.FullName : "userImages/defaultUser.png"; //kullanicinin yeni resmini güncelle
                    if (oldUserImage != "userImages/defaultUser.png") // Diger kullanicilarinda kullandigi ortak resmin kontrolü yapiliyor, ortak resim ise silme isleminin önüne geciyoruz
                    {
                        isNewImageUploaded = true;
                    }
                }

                var updatedUser = Mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await UserManager.UpdateAsync(updatedUser); // bilgileri db ye kaydediyoruz
                if (result.Succeeded) //Bu kullanıcı dogru sekilde db ye gönderilmisse,
                {
                    if (isNewImageUploaded) // yeni bir resim db ye eklendiyse
                    {
                        ImageHelper.Delete(oldUserImage); //eski resmi db den siliyoruz
                    }

                    var userUpdateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel //UpdateAsync isleminden sonra view e bir model dönüyoruz ki, frontend e kullanici bu bilgileri görsün
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message =
                                $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.",
                            User = updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateViewModel);
                }
                else //Kullanici güncelleme bilgileri db ye dogru sekilde yansimamissa,
                {
                    foreach (var error in result.Errors) //hatalar kullaniciya model üzerinden json a dönüstürülerek yansitilir
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var userUpdateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel //Basarisiz update isleminden sonra view e bir model dönüyoruz ki, frontend e kullanici bu bilgileri görsün
                    {
                        UserUpdateDto = userUpdateDto,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateErrorViewModel);
                }
            }
            var userUpdateModelStateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
            {
                UserUpdateDto = userUpdateDto,
                UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
            });
            return Json(userUpdateModelStateErrorViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            var updateDto = Mapper.Map<UserUpdateDto>(user);
            return View(updateDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<ViewResult> ChangeDetails(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewImageUploaded = false;
                var oldUser = await UserManager.GetUserAsync(HttpContext.User); //kullaniciyi güncellemeden bilgilerini oturumdan alip, burada sakliyoruz
                var oldUserImage = oldUser.Image; //kullanicinin eski resmini bir degiskene atiyoruz
                if (userUpdateDto.ImageFile != null) //Eger kullanici yeni bir resim yüklerse
                {
                    var uploadedImageDtoResult = await ImageHelper.Upload(userUpdateDto.UserName, userUpdateDto.ImageFile,ImageType.User);
                    userUpdateDto.Image = uploadedImageDtoResult.ResultStatus == ResultStatus.Success
                        ? uploadedImageDtoResult.Data.FullName : "userImages/defaultUser.png"; //kullanicinin yeni resmini güncelle
                    if (oldUserImage!= "userImages/defaultUser.png") // Diger kullanicilarinda kullandigi ortak resmin kontrolü yapiliyor, ortak resim ise silme isleminin önüne geciyoruz
                    {
                        isNewImageUploaded = true;
                    }
                }

                var updatedUser = Mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await UserManager.UpdateAsync(updatedUser); // bilgileri db ye kaydediyoruz
                if (result.Succeeded) //Bu kullanıcı dogru sekilde db ye gönderilmisse,
                {
                    if (isNewImageUploaded) // yeni bir resim db ye eklendiyse
                    {
                        ImageHelper.Delete(oldUserImage); //eski resmi db den siliyoruz
                    }
                    _toastNotification.AddSuccessToastMessage($"Bilgileriniz başarıyla güncellenmiştir.");
                    //TempData.Add("SuccessMessage", $"{ updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.");
                    return View(userUpdateDto);
                }
                else //Kullanici güncelleme bilgileri db ye dogru sekilde yansimamissa,
                {
                    foreach (var error in result.Errors) //hatalar kullaniciya model üzerinden json a dönüstürülerek yansitilir
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(userUpdateDto);
                }
            }
            return View(userUpdateDto);
        }

        [Authorize]
        [HttpGet]
        public ViewResult PasswordChange()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.GetUserAsync(HttpContext.User);
                var isVerified = await UserManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
                if (isVerified)
                {
                    var result = await UserManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword,
                        userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await UserManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                        //TempData.Add("SuccessMessage", $"Şifreniz başarıyla güncellenmiştir.");
                        _toastNotification.AddSuccessToastMessage($"Şifreniz başarıyla güncellenmiştir.");
                        return View();
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("",error.Description);
                        }

                        return View(userPasswordChangeDto);
                    }
                }
                else
                {
                    ModelState.AddModelError("","Lütfen girmiş olduğunuz şuanki şifrenizi kontrol ediniz.");
                    return View(userPasswordChangeDto);
                }
            }
            else
            {
                return View(userPasswordChangeDto);
            }
        }
    }
}
