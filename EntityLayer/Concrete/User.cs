using System.Collections;
using System.Collections.Generic;
using CoreLayer.Entities.Abstract;

namespace EntityLayer.Concrete
{
    public class User:EntityBase,IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public string UserName { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
