using System.Collections.Generic;
using EntityLayer.Concrete;

namespace EntityLayer.Dtos
{
    public class CommentListDto
    {
        public IList<Comment> Comments { get; set; }
    }
}
