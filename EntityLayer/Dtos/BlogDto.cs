using CoreLayer.Entities.Abstract;
using EntityLayer.Concrete;

namespace EntityLayer.Dtos
{
    public class BlogDto:DtoGetBase,IDto
    {
        public Blog Blog { get; set; }

        // public override ResultStatus ResultStatus { get; set; } = ResultStatus.Success; // ileride projemizde kullanibilir.
    }
}
