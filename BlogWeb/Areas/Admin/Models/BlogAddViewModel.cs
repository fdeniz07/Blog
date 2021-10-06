using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;

namespace BlogWeb.Areas.Admin.Models
{
    public class BlogAddViewModel
    {
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string Title { get; set; }

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MinLength(20, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string Content { get; set; }

        [DisplayName("Küçük Resim")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        public IFormFile ThumbnailFile { get; set; }

        [DisplayName("Resim")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        //[MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string Image { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Yazar Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        [MinLength(0, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string SeoAuthor { get; set; }

        [DisplayName("Makale Açıklaması")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        [MinLength(0, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string SeoDescription { get; set; }

        [DisplayName("Makale Etiketleri")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        [MinLength(0, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        public int CategoryId { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        public bool IsActive { get; set; }

        public IList<Category> Categories { get; set; }

        //yukaridaki alanlari yazmak yerine asagidaki yazim sekli de kullanilabilir

        //public ArticleAddDto ArticleAddDto { get; set; }
        //public CategoryListDto CategoryListDto { get; set; }
    }
}
