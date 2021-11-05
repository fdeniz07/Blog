using AutoMapper;
using BlogWeb.Areas.Admin.Models;
using EntityLayer.Concrete;
using EntityLayer.Dtos;

namespace BlogWeb.AutoMapper.Profiles
{
    public class ViewModelsProfile:Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<BlogAddViewModel, BlogAddDto>();
            CreateMap<BlogUpdateDto, BlogUpdateViewModel>().ReverseMap(); //ayni degerleri tersine de cevirmek icin reversemap metodu kullanilabilir.
            CreateMap<BlogRightSideBarWidgetOptions, BlogRightSideBarWidgetOptionsViewModel>();
        }
    }
}
