using CoreLayer.Entities.Abstract;

namespace EntityLayer.Concrete
{
    public class Category : EntityBase, IEntity
    {
        public string CategoryName { get; set; }

        public string Description { get; set; }


    }
}
