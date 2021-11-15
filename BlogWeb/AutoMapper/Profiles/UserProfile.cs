using AutoMapper;
using BlogWeb.Helpers.Abstract;
using EntityLayer.ComplexTypes;
using EntityLayer.Concrete;
using EntityLayer.Dtos;

namespace BlogWeb.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile() //II.Yol icin UserProfile(IImageHelper imageHelper) parametre veriyoruz. IImageHelper'in constructor icerisinde gecilebilmesi icin Startup.cs icerisinde gerekli kod satirini ekliyoruz.
        {
            CreateMap<UserAddDto, User>(); // Bizler bu kismi ImageHelper'in 1. yolu icin kullaniyoruz

            //CreateMap<UserAddDto, User>().ForMember(dest=>dest.Image,opt=>opt.MapFrom(x=>imageHelper.Upload(x.UserName,x.ImageFile,ImageType.User,null))); // Bircok kod satirini bir arada yazmak yerine tek bir kod satirinda sade bir sekilde yazmis oluyoruz.


            // Yukaridaki AutoMapper in coklu memberOption örneklerine su linkten ulasabiliriz: https://stackoverflow.com/questions/19226055/automapper-multiple-memberoptions

            CreateMap<User, UserUpdateDto>();

            CreateMap<UserUpdateDto, User>();

            //CreateMap<UserUpdateDto, User>().ForMember(dest=>dest.Image,opt=>opt.MapFrom(x=>imageHelper.Upload(x.UserName,x.ImageFile,ImageType.User,null)));
        }
    }
}
