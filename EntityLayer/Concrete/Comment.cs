using CoreLayer.Entities.Abstract;

namespace EntityLayer.Concrete
{
    public class Comment : EntityBase, IEntity
    {
       // public string UserName { get; set; }

        public string Content { get; set; } 
        
        public int BlogId { get; set; }
        public Blog Blog { get; set; } // Bir yorum bir makaleye sahip olmak zorundadir
    }
}
