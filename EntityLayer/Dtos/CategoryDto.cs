using CoreLayer.Entities.Abstract;
using EntityLayer.Concrete;

namespace EntityLayer.Dtos
{
    public class CategoryDto:DtoGetBase,IDto
    {
        public Category Category { get; set; }
    }
}
