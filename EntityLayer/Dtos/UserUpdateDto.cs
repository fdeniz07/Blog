using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CoreLayer.Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.Dtos
{
    public class UserUpdateDto : IDto
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

        [DisplayName("Adı")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(2, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string FirstName { get; set; }

        [DisplayName("Soyadı")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(2, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string LastName { get; set; }

        [DisplayName("Hakkında")]
        [MaxLength(1000, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string About { get; set; }

        [DisplayName("Twitter")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(20, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string TwitterLink { get; set; }

        [DisplayName("Facebook")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(20, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string FacebookLink { get; set; }

        [DisplayName("Instagram")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(20, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string InstagramLink { get; set; }

        [DisplayName("LinkedIn")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(20, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string LinkedInLink { get; set; }

        [DisplayName("Youtube")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(20, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string YoutubeLink { get; set; }

        [DisplayName("GitHub")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(20, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string GitHubLink { get; set; }

        [DisplayName("Website")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(20, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        public string WebsiteLink { get; set; }
    }
}
