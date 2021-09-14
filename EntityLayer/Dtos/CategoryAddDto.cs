using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adi")]
        [Required(ErrorMessage = "{0} bos gecilememelidir.")]
        [MaxLength(50,ErrorMessage = "{0} {1} karakterden fazla olmamalidir.")]
        [MinLength(3,ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        public string CategoryName { get; set; }

        [DisplayName("Kategori Aciklamasi")]
        [MaxLength(150, ErrorMessage = "{0} {1} karakterden fazla olmamalidir.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        public string Description { get; set; }

        [DisplayName("Kategori Özel Not Alani")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden fazla olmamalidir.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalidir.")]
        public string Note { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} bos gecilememelidir.")]
        public bool IsActive { get; set; }
    }
}
