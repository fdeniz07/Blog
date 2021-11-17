using BlogWeb.Areas.Admin.Models;
using BlogWeb.Models;
using FluentValidation;

namespace BlogWeb.Areas.Admin.ValidationRules.FluentValidation
{
    public class BlogRightSideBarWidgetOptionsViewModelValidator : AbstractValidator<BlogRightSideBarWidgetOptionsViewModel>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        public BlogRightSideBarWidgetOptionsViewModelValidator()
        {
            RuleFor(x => x.Header).NotEmpty().WithMessage("Widget Başlığı" + NotEmptyMessage).MaximumLength(150)
                .WithMessage("Widget Başlığı alanı 150 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Widget Başlığı alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.TakeSize).NotEmpty().WithMessage("Makale Sayısı" + NotEmptyMessage).InclusiveBetween(0, 50)
                .WithMessage("Makale Sayısı alanı en az 0, en fazla 50 olmalıdır.");


            RuleFor(x => x.CategoryId).NotNull().WithMessage("Kategori" + NotEmptyMessage);

            RuleFor(x => x.FilterBy).NotNull().WithMessage("Filtre Türü" + NotEmptyMessage);

            RuleFor(x => x.OrderBy).NotNull().WithMessage("Sıralama Türü" + NotEmptyMessage);

            RuleFor(x => x.IsAscending).NotNull().WithMessage("Sıralama Ölçütü" + NotEmptyMessage);

            RuleFor(x => x.StartAt).NotEmpty().WithMessage("Başlangıç Tarihi" + NotEmptyMessage);

            RuleFor(x => x.EndAt).NotEmpty().WithMessage("Bitiş Tarihi" + NotEmptyMessage);

            RuleFor(x => x.MaxViewCount).NotNull().WithMessage("Maksimum Okunma Sayısı" + NotEmptyMessage);

            RuleFor(x => x.MinViewCount).NotNull().WithMessage("Minimum Okunma Sayısı" + NotEmptyMessage);

            RuleFor(x => x.MaxCommentCount).NotNull().WithMessage("Maksimum Yorum Sayısı" + NotEmptyMessage);

            RuleFor(x => x.MinCommentCount).NotNull().WithMessage("Minimum Yorum Sayısı" + NotEmptyMessage);
        }
    }
}
