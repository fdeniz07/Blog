namespace EntityLayer.ComplexTypes
{
    public enum OrderBy
    {
        //FilterBy ile kullanicinin makalelerini getirebilir ve bunlari da asagidaki alanlara göre siralama yapabiliriz.

        Date=0, // Tarihe göre filtreleme yapmak isteyebiliriz

        ViewCount=1, // Kullanicinin en cok okunan makaleleri

        CommentCount=2 //En cok yorum alan makaleler
    }
}
