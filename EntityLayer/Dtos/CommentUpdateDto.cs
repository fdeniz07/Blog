using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CoreLayer.Entities.Abstract;

namespace EntityLayer.Dtos
{
    public class CommentUpdateDto : IDto
    {
        //[Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public int Id { get; set; }

        //[DisplayName("Yorum")]
        //[Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        //[MaxLength(1000, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        //[MinLength(2, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string Content { get; set; }

        //[DisplayName("Aktif Mi?")]
        //[Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public bool IsActive { get; set; }

        //[Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public int BlogId { get; set; }
    }
}
