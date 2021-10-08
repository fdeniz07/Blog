using System.Threading.Tasks;
using BlogWeb.Areas.Admin.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace BlogWeb.Areas.Admin.ViewComponents
{
    public class AdminMenuViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public AdminMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        /* Yapimiz senkron olsaydi
         *
         *public ViewViewComponentResult Invoke()
           {
                   var roles = _userManager.GetRolesAsync(user).Result;

                    return View(new UserWithRolesViewModel
                    {
                        User = user,
                        Roles = roles
                    });
           }
         */

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null)
            {
                return Content("Kullanıcı bulunamadı.");
            }

            if (roles == null)
            {
                return Content("Roller bulunamadı.");
            }

            return View(new UserWithRolesViewModel
            {
                User = user,
                Roles = roles
            });
        }
    }
}
