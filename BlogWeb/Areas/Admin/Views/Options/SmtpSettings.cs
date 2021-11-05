using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Concrete
{
    public class SmtpSettings
    {
        [DisplayName("Sunucu")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string Server { get; set; }
        [DisplayName("Port")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [Range(0,9999,ErrorMessage = "{0} alanı en az {1}, en fazla {2} değerinde olmalıdır.")]
        public int Port { get; set; }
        [DisplayName("Gönderen Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(2, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string SenderName { get; set; }
        [DisplayName("Gönderen Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [DataType(DataType.EmailAddress,ErrorMessage = "{0} alanı e-posta formatında olmalıdır.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string SenderEmail { get; set; }
        [DisplayName("Kullanıcı Adı/E-Posta")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(1, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string Username { get; set; }
        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [DataType(DataType.Password, ErrorMessage = "{0} alanı şifre formatında olmalıdır.")]
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string Password { get; set; }
    }
}
