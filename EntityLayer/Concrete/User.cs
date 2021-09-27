using System.Collections;
using System.Collections.Generic;
using CoreLayer.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class User:IdentityUser<int>
    {
        public string Image { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
