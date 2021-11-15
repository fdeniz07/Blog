using System.Collections;
using System.Collections.Generic;
using CoreLayer.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class User:IdentityUser<int>, IEntity
    {
        public string Image { get; set; }

        public ICollection<Blog> Blogs { get; set; }

        public string About { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string YoutubeLink { get; set; }

        public string TwitterLink { get; set; }

        public string InstagramLink { get; set; }

        public string FacebookLink { get; set; }

        public string LinkedInLink { get; set; }

        public string GitHubLink { get; set; }

        public string WebsiteLink { get; set; }
    }
}
