using System.Threading.Tasks;
using BlogWeb.Areas.Admin.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace BlogWeb.Areas.Admin.ViewComponents
{
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public UserMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        /* Yapimiz senkron olsaydi
         *
         *public ViewViewComponentResult Invoke()
           {
                var user = _userManager.GetUserAsync(HttpContext.User).Result;
                return View(new UserViewModel
                {
                    User = user
                });
           }
         */


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Content("Kullanıcı bulunamadı.");
            }

            return View(new UserViewModel
            {
                User = user
            });
        }
    }
}
