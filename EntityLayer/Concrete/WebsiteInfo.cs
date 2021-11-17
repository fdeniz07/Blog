using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class WebsiteInfo
    {
        public string Title { get; set; }

        public string MenuTitle { get; set; }

        public string SeoAuthor { get; set; }

        public string SeoTags { get; set; }

        public string SeoDescription { get; set; }


        /* Attribute'ler yerine FluentValidation kullanilmasinin nedenleri
         *
         * Separation Of Concern (SoC): Ilgili kodlar ilgili yerlerde tutulacak ve karman corman kod yazilmayacak.
         * Clean Code, SOLID ve Unit Test icin tavsiye edilir
         */

        //[DisplayName("Site Adı/Başlığı")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        //[MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        //[MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        //public string Title { get; set; }

        //[DisplayName("Menü Üzerindeki Site Adı/Başlığı")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        //[MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        //[MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        //public string MenuTitle { get; set; }

        //[DisplayName("Seo Yazar")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        //[MaxLength(60, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        //[MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        //public string SeoAuthor { get; set; }

        //[DisplayName("Seo Etiketleri")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        //[MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        //[MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        //public string SeoTags { get; set; }

        //[DisplayName("Seo Açıklama")]
        //[Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        //[MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        //[MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        //public string SeoDescription { get; set; }

    }
}
