using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class BlogRightSideBarWidgetOptionsValidator : AbstractValidator<BlogRightSideBarWidgetOptions>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        public BlogRightSideBarWidgetOptionsValidator()
        {
            RuleFor(x => x.Header).NotEmpty().WithMessage("Widget Başlığı" + NotEmptyMessage).MaximumLength(150)
                .WithMessage("Widget Başlığı alanı 150 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Widget Başlığı alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.TakeSize).NotEmpty().WithMessage("Makale Sayısı" + NotEmptyMessage).InclusiveBetween(0, 50)
                .WithMessage("Makale Sayısı alanı en az 0, en fazla 50 olmalıdır.");


            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori" + NotEmptyMessage);

            RuleFor(x => x.FilterBy).NotEmpty().WithMessage("Filtre Türü" + NotEmptyMessage);

            RuleFor(x => x.OrderBy).NotEmpty().WithMessage("Sıralama Türü" + NotEmptyMessage);

            RuleFor(x => x.IsAscending).NotEmpty().WithMessage("Sıralama Ölçütü" + NotEmptyMessage);

            RuleFor(x => x.StartAt).NotEmpty().WithMessage("Başlangıç Tarihi" + NotEmptyMessage);

            RuleFor(x => x.EndAt).NotEmpty().WithMessage("Bitiş Tarihi" + NotEmptyMessage);

            RuleFor(x => x.MaxViewCount).NotEmpty().WithMessage("Maksimum Okunma Sayısı" + NotEmptyMessage);

            RuleFor(x => x.MinViewCount).NotEmpty().WithMessage("Minimum Okunma Sayısı" + NotEmptyMessage);

            RuleFor(x => x.MaxCommentCount).NotEmpty().WithMessage("Maksimum Yorum Sayısı" + NotEmptyMessage);

            RuleFor(x => x.MinCommentCount).NotEmpty().WithMessage("Minimum Yorum Sayısı" + NotEmptyMessage);
        }
    }
}
