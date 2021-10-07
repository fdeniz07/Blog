using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CoreLayer.Entities.Abstract;

namespace EntityLayer.Concrete
{
    public class Category : EntityBase, IEntity
    {
        public override bool IsDeleted { get; set; } = false;

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
