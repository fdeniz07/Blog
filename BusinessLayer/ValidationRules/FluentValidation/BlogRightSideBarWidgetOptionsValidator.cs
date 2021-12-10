using BusinessLayer.Utilities;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class BlogRightSideBarWidgetOptionsValidator : AbstractValidator<BlogRightSideBarWidgetOptions>
    {
        private readonly ValidatorMessages _validatorMessages = new ValidatorMessages();

        public BlogRightSideBarWidgetOptionsValidator()
        {
            RuleFor(x => x.Header).NotEmpty().WithName("Widget Başlığı").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(150).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.TakeSize).NotEmpty().WithName("Makale Sayısı").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).InclusiveBetween(0, 50).WithMessage("{PropertyName} en az 0, en fazla {MaxLength} olmalıdır.");


            RuleFor(x => x.CategoryId).NotEmpty().WithName("Kategori").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

            RuleFor(x => x.FilterBy).NotEmpty().WithName("Filtre Türü").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

            RuleFor(x => x.OrderBy).NotEmpty().WithName("Sıralama Türü").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

            RuleFor(x => x.IsAscending).NotEmpty().WithName("Sıralama Ölçütü").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

            RuleFor(x => x.StartAt).NotEmpty().WithName("Başlangıç Tarihi").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

            RuleFor(x => x.EndAt).NotEmpty().WithName("Bitiş Tarihi").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

            RuleFor(x => x.MaxViewCount).NotEmpty().WithName("Maksimum Okunma Sayısı").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

            RuleFor(x => x.MinViewCount).NotEmpty().WithName("Minimum Okunma Sayısı").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

            RuleFor(x => x.MaxCommentCount).NotEmpty().WithName("Maksimum Yorum Sayısı").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

            RuleFor(x => x.MinCommentCount).NotEmpty().WithName("Minimum Yorum Sayısı").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);
        }
    }
}
