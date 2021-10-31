using EntityLayer.Concrete;
using System.Collections.Generic;
using CoreLayer.Entities.Abstract;

namespace EntityLayer.Dtos
{
    public class RoleListDto : IDto
    {
        public IList<Role> Roles { get; set; }
    }
}
