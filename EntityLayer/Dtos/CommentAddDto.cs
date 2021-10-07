using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Dtos
{
    public class CommentAddDto
    {
        [DisplayName("Yorum")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(1000, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(2, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string Content { get; set; }

        [DisplayName("Adınız")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(2, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string CreatedByName { get; set; }

        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public int BlogId { get; set; }
    }
}
