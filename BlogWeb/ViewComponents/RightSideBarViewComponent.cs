using System.Threading.Tasks;
using BlogWeb.Models;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.ViewComponents
{
    public class RightSideBarViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;

        public RightSideBarViewComponent(ICategoryService categoryService, IBlogService blogService)
        {
            _categoryService = categoryService;
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            var blogsResult = await _blogService.GetAllByViewCountAsync(isAscending: false, takeSize: 5); // Azalan bir sekilde 5 makale gelecek

            return View(new RightSideBarViewModel
            {
                Categories = categoriesResult.Data.Categories,
                Blogs = blogsResult.Data.Blogs
            });
        }
    }
}
