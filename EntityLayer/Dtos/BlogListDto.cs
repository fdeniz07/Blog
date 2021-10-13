using System;
using CoreLayer.Entities.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace EntityLayer.Dtos
{
    public class BlogListDto : DtoGetBase
    {
        public IList<Blog> Blogs { get; set; }

        public int? CategoryId { get; set; } 
    }
}
