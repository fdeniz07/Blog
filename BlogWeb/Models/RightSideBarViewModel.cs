using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BlogWeb.Models
{
    public class RightSideBarViewModel
    {
        public IList<Category> Categories { get; set; }

        public IList<Blog> Blogs { get; set; }
    }
}
