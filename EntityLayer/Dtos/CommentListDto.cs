using System.Collections.Generic;
using CoreLayer.Entities.Abstract;
using EntityLayer.Concrete;

namespace EntityLayer.Dtos
{
    public class CommentListDto : IDto
    {
        public IList<Comment> Comments { get; set; }
    }
}
