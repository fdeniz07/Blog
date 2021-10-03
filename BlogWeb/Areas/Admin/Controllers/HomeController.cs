using System.Threading.Tasks;
using BlogWeb.Areas.Admin.Models;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public HomeController(ICategoryService categoryService, IBlogService blogService, ICommentService commentService, UserManager<User> userManager)
        {
            _categoryService = categoryService;
            _blogService = blogService;
            _commentService = commentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var categoriesCountResult = await _categoryService.CountByNonDeletedAsync();
            var blogsCountResult = await _blogService.CountByNonDeletedAsync();
            var commentsCountResult = await _commentService.CountByNonDeletedAsync();
            var usersCount = await _userManager.Users.CountAsync();
            var blogsResult = await _blogService.GetAllAsync();

            if (categoriesCountResult.ResultStatus == ResultStatus.Success && blogsResult.ResultStatus == ResultStatus.Success && commentsCountResult.ResultStatus == ResultStatus.Success && usersCount > -1 && blogsCountResult.ResultStatus == ResultStatus.Success)
            {
                return View(new DashboardViewModel
                {
                    CategoriesCount = categoriesCountResult.Data,
                    BlogsCount = blogsCountResult.Data,
                    CommentsCount = commentsCountResult.Data,
                    UsersCount = usersCount,
                    Blogs = blogsResult.Data
                });
            }
            return NotFound();
        }
    }
}
