using CoreLayer.Entities.Abstract;

namespace EntityLayer.Concrete
{
    public class Blog : EntityBase, IEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string ThumbnailImage { get; set; }

        public string Image { get; set; }
    }
}
