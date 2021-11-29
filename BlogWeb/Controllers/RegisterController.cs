using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using BlogWeb.Areas.Admin.Models;
using BlogWeb.Helpers.Abstract;
using BlogWeb.Models;
using CoreLayer.Utilities.Extensions;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.ComplexTypes;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BlogWeb.Controllers
{
    public class RegisterController : BlogBaseController
    {
        private readonly IToastNotification _toastNotification;

        public RegisterController(UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper,
            IToastNotification toastNotification) : base(userManager, mapper, imageHelper)
        {
            _toastNotification = toastNotification;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return PartialView("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                var uploadedImageDtoResult =
                await ImageHelper.Upload(userAddDto.UserName, userAddDto.ImageFile, ImageType.User);
                userAddDto.Image = uploadedImageDtoResult.ResultStatus == ResultStatus.Success
                    ? uploadedImageDtoResult.Data.FullName
                    : "userImages/defaultUser.png";
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
                        UserAddPartial = await this.RenderViewToStringAsync("Index", userAddDto)
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
                        UserAddPartial = await this.RenderViewToStringAsync("Index", userAddDto)
                    });
                    return Json(userAddAjaxErrorModel);
                }
            }

            var userAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("Index", userAddDto)
            });
            return Json(userAddAjaxModelStateErrorModel);
        }
    }
}
