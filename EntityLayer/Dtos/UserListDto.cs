using System.Collections.Generic;
using CoreLayer.Entities.Abstract;
using EntityLayer.Concrete;

namespace EntityLayer.Dtos
{
    public class UserListDto:DtoGetBase
    {
        public IList<User> Users { get; set; }  
    }
}
