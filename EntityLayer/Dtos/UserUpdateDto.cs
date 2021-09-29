using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public int Id { get; set; }
        
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string UserName { get; set; }

        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(14, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")] //+4917612345678
        [MinLength(12, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Resim Ekle")]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }

        [DisplayName("Resim")]
        public string Image { get; set; }
    }
}
