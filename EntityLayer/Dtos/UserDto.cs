using CoreLayer.Entities.Abstract;
using EntityLayer.Concrete;

namespace EntityLayer.Dtos
{
    public class UserDto : DtoGetBase, IDto
    {
        public User User { get; set; }
    }
}
