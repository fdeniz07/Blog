using EntityLayer.Concrete;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Dtos
{
    public class BlogUpdateDto
    {
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        public int Id { get; set; }

        [DisplayName("Baslik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden büyük olmamalidir")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kücük olmamalidir")]
        public string Title { get; set; }

        [DisplayName("Icerik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MinLength(20, ErrorMessage = "{0} alani {1} karakterden kücük olmamalidir")]
        public string Content { get; set; }

        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(250, ErrorMessage = "{0} alani {1} karakterden büyük olmamalidir")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kücük olmamalidir")]
        public string Thumbnail { get; set; }

        [DisplayName("Resim")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden büyük olmamalidir")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden kücük olmamalidir")]
        public string Image { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Seo Yazar Bilgisi")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(50, ErrorMessage = "{0} alani {1} karakterden büyük olmamalidir")]
        [MinLength(0, ErrorMessage = "{0} alani {1} karakterden kücük olmamalidir")]
        public string SeoAuthor { get; set; }

        [DisplayName("Seo Aciklama Bilgisi")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(150, ErrorMessage = "{0} alani {1} karakterden büyük olmamalidir")]
        [MinLength(0, ErrorMessage = "{0} alani {1} karakterden kücük olmamalidir")]
        public string SeoDescription { get; set; }

        [DisplayName("Seo Etiket Bilgisi")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alani {1} karakterden büyük olmamalidir")]
        [MinLength(0, ErrorMessage = "{0} alani {1} karakterden kücük olmamalidir")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        public bool IsActive { get; set; }

        [DisplayName("Silindi Mi?")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir")]
        public bool IsDelete { get; set; }
    }
}
