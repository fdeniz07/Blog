using CoreLayer.Entities.Abstract;

namespace EntityLayer.Concrete
{
    public class About : EntityBase, IEntity
    {
        public string Details1 { get; set; }
        public string Details2 { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string MapLocation { get; set; }
    }
}
