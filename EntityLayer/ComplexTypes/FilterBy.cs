namespace EntityLayer.ComplexTypes
{
    public enum FilterBy
    {
        Category=0, //GetAllByUserIdOnData(FilterBy = FilterBy.Category, int categoryId) -- Bunu kullanma amacimiz, bir makale acildiginda, ilgil kategoriye ait baska makalelerinde gözükmesini isteyebiliriz.

        Date=1, // Tarihe göre filtreleme yapmak isteyebiliriz

        ViewCount=2, // Kullanicinin en cok okunan makaleleri

        CommentCount=3 //En cok yorum alan makaleler
    }
}
