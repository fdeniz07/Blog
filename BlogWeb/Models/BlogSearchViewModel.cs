using EntityLayer.Dtos;

namespace BlogWeb.Models
{
    public class BlogSearchViewModel
    {
        public BlogListDto BlogListDto { get; set; }

        public string Keyword { get; set; }
    }
}
