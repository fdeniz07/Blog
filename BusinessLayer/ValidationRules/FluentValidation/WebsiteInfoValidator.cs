using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class WebsiteInfoValidator:AbstractValidator<WebsiteInfo>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        //public string MaxLengthMessage { get; } = " {0} karakterden büyük olmamalıdır.";

        //public string MinLengthMessage { get; } = "karakterden küçük olmamalıdır.";

        public WebsiteInfoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Site Adı/Başlığı" + NotEmptyMessage).MaximumLength(100)
                .WithMessage("Site Adı/Başlığı alanı 100 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Site Adı/Başlığı alanı 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.MenuTitle).NotEmpty().WithMessage("Menü Üzerindeki Site Adı/Başlığı" + NotEmptyMessage).MaximumLength(100)
                .WithMessage("Menü Üzerindeki Site Adı/Başlığı alanı 100 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Menü Üzerindeki Site Adı/Başlığı alanı 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.SeoAuthor).NotEmpty().WithMessage("Seo Yazar" + NotEmptyMessage).MaximumLength(60)
                .WithMessage("Seo Yazar alanı 60 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Seo Yazar alanı 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.SeoTags).NotEmpty().WithMessage("Seo Etiketleri" + NotEmptyMessage).MaximumLength(150)
                .WithMessage("Seo Etiketleri alanı 150 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Seo Etiketleri alanı 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.SeoDescription).NotEmpty().WithMessage("Seo Açıklama" + NotEmptyMessage).MaximumLength(100)
                .WithMessage("Seo Açıklama alanı 100 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Seo Açıklama alanı 5 karakterden küçük olmamalıdır.");

        }

    }
}
