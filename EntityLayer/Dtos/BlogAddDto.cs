using CoreLayer.Entities.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.Dtos
{
    public class BlogAddDto : IDto
    {
        //[DisplayName("Başlık")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        //[MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        //[MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string Title { get; set; }

        //[DisplayName("İçerik")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        //[MinLength(20, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string Content { get; set; }

        //[DisplayName("Thumbnail")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        //[MaxLength(250, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        //[MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string Thumbnail { get; set; }

        //[DisplayName("Resim")]
        ////[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        //[MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        public string Image { get; set; }

        //[DisplayName("Tarih")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        //[DisplayName("Seo Yazar Bilgisi")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        //[MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        //[MinLength(0, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string SeoAuthor { get; set; }

        //[DisplayName("Seo Açıklama Bilgisi")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        //[MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        //[MinLength(0, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string SeoDescription { get; set; }

        //[DisplayName("Seo Etiket Bilgisi")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        //[MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır")]
        //[MinLength(0, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır")]
        public string SeoTags { get; set; }

        //[DisplayName("Kategori")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        //[DisplayName("Aktif Mi?")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        public bool IsActive { get; set; }
    }
}
