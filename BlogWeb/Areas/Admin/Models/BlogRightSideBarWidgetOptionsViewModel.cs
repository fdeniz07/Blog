using EntityLayer.ComplexTypes;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogWeb.Areas.Admin.Models
{
    public class BlogRightSideBarWidgetOptionsViewModel
    {
        //[DisplayName("Widget Başlığı")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        //[MaxLength(150, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        //[MinLength(5, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Header { get; set; }

        //[DisplayName("Makale Sayısı")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        //[Range(0, 50, ErrorMessage = "{0} alanı en az {1}, en fazla {2} olmalıdır.")]
        public int TakeSize { get; set; }

        //[DisplayName("Kategori")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int CategoryId { get; set; }

        //[DisplayName("Filtre Türü")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        public FilterBy FilterBy { get; set; }

        //[DisplayName("Sıralama Türü")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        public OrderBy OrderBy { get; set; }

        //[DisplayName("Sıralama Ölçütü")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        public bool IsAscending { get; set; }

        //[DisplayName("Başlangıç Tarihi")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        //[DataType(DataType.Date, ErrorMessage = "{0} alanı tarih formatında olmalıdır.")]
        public DateTime StartAt { get; set; }

        //[DisplayName("Bitiş Tarihi")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        //[DataType(DataType.Date, ErrorMessage = "{0} alanı tarih formatında olmalıdır.")]
        public DateTime EndAt { get; set; }

        //[DisplayName("Maksimum Okunma Sayısı")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int MaxViewCount { get; set; }

        //[DisplayName("Minimum Okunma Sayısı")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int MinViewCount { get; set; }

        //[DisplayName("Maksimum Yorum Sayısı")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int MaxCommentCount { get; set; }

        //[DisplayName("Minimum Yorum Sayısı")]
        //[Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int MinCommentCount { get; set; }

        public IList<Category> Categories { get; set; }
    }
}
