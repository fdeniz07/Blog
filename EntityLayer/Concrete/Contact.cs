using CoreLayer.Entities.Abstract;

namespace EntityLayer.Concrete
{
    public class Contact:EntityBase,IEntity
    {
        public string UserName { get; set; }

        public string Mail { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
