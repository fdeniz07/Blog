using System.Collections.Generic;
using EntityLayer.Concrete;
using EntityLayer.Dtos;

namespace BlogWeb.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }
        public int BlogsCount { get; set; }
        public int CommentsCount { get; set; }
        public int UsersCount { get; set; }
        public BlogListDto Blogs { get; set; }
    }
}
