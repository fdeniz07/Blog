using System.ComponentModel.DataAnnotations;

namespace EntityLayer.ComplexTypes
{
    public enum OrderBy
    {
        //FilterBy ile kullanicinin makalelerini getirebilir ve bunlari da asagidaki alanlara göre siralama yapabiliriz.

        [Display(Name = "Tarih")]
        Date =0, // Tarihe göre filtreleme yapmak isteyebiliriz

        [Display(Name = "Okunma Sayısı")]
        ViewCount =1, // Kullanicinin en cok okunan makaleleri

        [Display(Name = "Yorum Sayısı")]
        CommentCount =2 //En cok yorum alan makaleler
    }
}
