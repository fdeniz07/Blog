using CoreLayer.Entities.Abstract;

namespace EntityLayer.Concrete
{
    public class Author : UserBase, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string About { get; set; }

        public string Mail { get; set; }
        
        public string Image { get; set; }
    }
}
