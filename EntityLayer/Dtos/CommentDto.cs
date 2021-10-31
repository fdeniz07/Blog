using CoreLayer.Entities.Abstract;
using EntityLayer.Concrete;

namespace EntityLayer.Dtos
{
    public class CommentDto : IDto
    {
        public Comment Comment { get; set; }
    }
}
