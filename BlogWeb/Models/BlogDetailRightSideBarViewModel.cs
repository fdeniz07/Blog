using EntityLayer.Concrete;
using EntityLayer.Dtos;

namespace BlogWeb.Models
{
    public class BlogDetailRightSideBarViewModel
    {
        public string Header { get; set; }

        public BlogListDto BlogListDto { get; set; }

        public User User { get; set; }
    }
}
