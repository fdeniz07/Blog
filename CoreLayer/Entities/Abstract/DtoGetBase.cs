using System;
using CoreLayer.Utilities.Results.ComplexTypes;

namespace CoreLayer.Entities.Abstract
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }
        public int CurrentPage { get; set; } = 1;  //Sayfalama yapisi icin ilk deger atamasi

        public int PageSize { get; set; } = 6; // Sayfalama basina düsecek degerler

        public int TotalCount { get; set; } //Toplam Entity sayisi

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));  //Toplam Sayfa Sayisi

        public bool ShowPrevious => CurrentPage > 1; // Geri tusu

        public bool ShowNext => CurrentPage < TotalPages; // Ileri tusu
    }
}
