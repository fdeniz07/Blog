using System;
using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.Dtos;

namespace BusinessLayer.AutoMapper.Profiles
{
    public class BlogProfile:Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogAddDto, Blog>().ForMember(dest=>dest.CreatedDate,opt=>opt.MapFrom(x=> DateTime.Now)); //Burada amac; blog icerisinde CreatedDate alani var ama Dto da yok. Bizim verecegimiz islemlerle bu dönüstürme islemlerini gerceklestiriyor

            CreateMap<BlogUpdateDto, Blog>().ForMember(dest=>dest.ModifiedDate,opt=>opt.MapFrom(x=>DateTime.Now));
           
            CreateMap<Blog, BlogUpdateDto>();
        }
    }
}
