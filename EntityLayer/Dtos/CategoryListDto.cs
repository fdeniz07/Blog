using CoreLayer.Entities.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace EntityLayer.Dtos
{
    public class CategoryListDto:DtoGetBase,IDto
    {
        public IList<Category> Categories { get; set; }
    }
}
