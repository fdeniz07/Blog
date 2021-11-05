using System.ComponentModel.DataAnnotations;

namespace EntityLayer.ComplexTypes
{
    public enum FilterBy
    {
        [Display(Name="Kategori")]
        Category=0, //GetAllByUserIdOnData(FilterBy = FilterBy.Category, int categoryId) -- Bunu kullanma amacimiz, bir makale acildiginda, ilgil kategoriye ait baska makalelerinde gözükmesini isteyebiliriz.

        [Display(Name = "Tarih")]
        Date =1, // Tarihe göre filtreleme yapmak isteyebiliriz

        [Display(Name = "Okunma Sayısı")]
        ViewCount =2, // Kullanicinin en cok okunan makaleleri

        [Display(Name = "Yorum Sayısı")]
        CommentCount =3 //En cok yorum alan makaleler
    }
}
