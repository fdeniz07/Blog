using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class AboutUsPageInfoValidator : AbstractValidator<AboutUsPageInfo>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        public AboutUsPageInfoValidator()
        {
            RuleFor(x => x.Header).NotEmpty().WithMessage("Başlık" + NotEmptyMessage).MaximumLength(150)
                .WithMessage("Başlık alanı 150 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Başlık alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik" + NotEmptyMessage).MaximumLength(1500)
                .WithMessage("İçerik alanı 1500 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("İçerik alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.SeoAuthor).NotEmpty().WithMessage("Seo Yazar Bilgisi" + NotEmptyMessage).MaximumLength(60)
                .WithMessage("Seo Yazar Bilgisi alanı 60 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Seo Yazar Bilgisi alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.SeoDescription).NotEmpty().WithMessage("Seo Açıklama Bilgisi" + NotEmptyMessage).MaximumLength(100)
                .WithMessage("Seo Açıklama Bilgisi alanı 100 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Seo Açıklama Bilgisi alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.SeoTags).NotEmpty().WithMessage("Seo Etiket Bilgisi" + NotEmptyMessage).MaximumLength(100)
                .WithMessage("Seo Etiket Bilgisi alanı 100 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Seo Etiket Bilgisi alanı 5 karakterden küçük olmamalıdır.");
        }
    }
}
