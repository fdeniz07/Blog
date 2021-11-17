using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWeb.Areas.Admin.Models;
using FluentValidation;

namespace BlogWeb.Areas.Admin.ValidationRules.FluentValidation
{
    public class BlogAddViewModelValidator:AbstractValidator<BlogAddViewModel>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        public BlogAddViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık" + NotEmptyMessage).MaximumLength(100).WithMessage("Başlık alanı 100 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Başlık alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik" + NotEmptyMessage).MinimumLength(20).WithMessage("İçerik alanı 20 karakterden küçük olmamalıdır.");


            RuleFor(x => x.ThumbnailFile).NotEmpty().WithMessage("Küçük Resim" + NotEmptyMessage);


            RuleFor(x => x.Image).MaximumLength(100).WithMessage("Resim alanı 100 karakterden büyük olmamalıdır.");


            RuleFor(x => x.Date).NotEmpty().WithMessage("Tarih" + NotEmptyMessage);


            RuleFor(x => x.SeoAuthor).NotEmpty().WithMessage("Seo Yazar Bilgisi" + NotEmptyMessage).MaximumLength(50)
                .WithMessage("Seo Yazar Bilgisi alanı 50 karakterden büyük olmamalıdır.").MinimumLength(0).WithMessage("Seo Yazar Bilgisi alanı 0 karakterden küçük olmamalıdır.");


            RuleFor(x => x.SeoDescription).NotEmpty().WithMessage("Seo Açıklama Bilgisi" + NotEmptyMessage).MaximumLength(150)
                .WithMessage("Seo Açıklama Bilgisi alanı 150 karakterden büyük olmamalıdır.").MinimumLength(0).WithMessage("Seo Açıklama Bilgisi alanı 0 karakterden küçük olmamalıdır.");


            RuleFor(x => x.SeoTags).NotEmpty().WithMessage("Seo Etiket Bilgisi" + NotEmptyMessage).MaximumLength(100).WithMessage("Seo Etiket Bilgisi alanı 100 karakterden büyük olmamalıdır.").MinimumLength(0).WithMessage("Seo Etiket Bilgisi alanı 0 karakterden küçük olmamalıdır.");


            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori" + NotEmptyMessage);


            RuleFor(x => x.IsActive).NotEmpty().WithMessage("Aktif Mi?" + NotEmptyMessage);
        }
    }
}
