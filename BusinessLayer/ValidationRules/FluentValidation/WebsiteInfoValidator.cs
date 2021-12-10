using BusinessLayer.Utilities;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class WebsiteInfoValidator:AbstractValidator<WebsiteInfo>
    {
        private readonly ValidatorMessages _validatorMessages = new ValidatorMessages();

        public WebsiteInfoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithName("Site Adı/Başlığı").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(100)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);

            RuleFor(x => x.MenuTitle).NotEmpty().WithName("Menü Üzerindeki Site Adı/Başlığı").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);

            RuleFor(x => x.SeoAuthor).NotEmpty().WithName("Seo Yazar").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(60)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);

            RuleFor(x => x.SeoTags).NotEmpty().WithName("Seo Etiketleri").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(150)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);

            RuleFor(x => x.SeoDescription).NotEmpty().WithName("Seo Açıklama").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);

        }

    }
}
