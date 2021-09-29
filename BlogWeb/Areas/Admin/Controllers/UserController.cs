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
using Microsoft.AspNetCore.Http;

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
            //ImageDelete("test");
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
                userAddDto.Image = await ImageUpload(userAddDto.UserName,userAddDto.ImageFile);
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

        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
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

        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewImageUploaded = false;
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString()); //kullaniciyi güncellemeden bilgilerini burada sakliyoruz
                var oldUserImage = oldUser.Image; //kullanicinin eski resmini bir degiskene atiyoruz
                if (userUpdateDto.ImageFile != null) //Eger kullanici yeni bir resim yüklerse
                {
                    userUpdateDto.Image = await ImageUpload(userUpdateDto.UserName, userUpdateDto.ImageFile); //kullanicinin yeni resmini güncelle
                    isNewImageUploaded = true; //kullanicinin yeni resim eklediginde durumunu güncelliyoruz
                }

                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser); // bilgileri db ye kaydediyoruz
                if (result.Succeeded) //Bu kullanıcı dogru sekilde db ye gönderilmisse,
                {
                    if (isNewImageUploaded) // yeni bir resim db ye eklendiyse
                    {
                        ImageDelete(oldUserImage); //eski resmi db den siliyoruz
                    }

                    var userUpdateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel //Update isleminden sonra view e bir model dönüyoruz ki, frontend e kullanici bu bilgileri görsün
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
                        ModelState.AddModelError("",error.Description);
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


        public async Task<string> ImageUpload(string userName, IFormFile imageFile)
        {
            // ~/img/user.Picture
            string wwwroot = _env.WebRootPath;

            //string fileName2 = Path.GetFileNameWithoutExtension(ImageFile.FileName); // fatihdeniz
            string fileExtension = Path.GetExtension(imageFile.FileName); //.png
            DateTime dateTime = DateTime.Now;

            //FatihDeniz_601_5_38_12_28_09_2021_userFatihDenizResmi.png
            //string fileName = $"{UserName}_{dateTime.FullDateAndTimeStringWithUnderscore()}_{fileName2}";

            //FatihDeniz_601_5_38_12_28_09_2021.png
            string fileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return fileName; // FatihDeniz_587_5_38_12_28_09_2021.png - "~/img/user.Image"
        }

        public bool ImageDelete(string imageName)
        {
            // Amac : Kullanici resmini güncelledikten sonra eski resmin sunucudan silinmesi veyahut silinen bir kullanicinin resmininde silinmesi

            //imageName = "ahmetak_422_18_19_21_28_9_2021.png";
            string wwwroot = _env.WebRootPath;
            var fileToDelete = Path.Combine($"{wwwroot}/img", imageName);
            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
