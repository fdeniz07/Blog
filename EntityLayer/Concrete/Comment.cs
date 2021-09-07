using CoreLayer.Entities.Abstract;

namespace EntityLayer.Concrete
{
    public class Comment : EntityBase, IEntity
    {
        public string UserName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
