using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.Dtos;

namespace BlogWeb.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
        }
    }
}
