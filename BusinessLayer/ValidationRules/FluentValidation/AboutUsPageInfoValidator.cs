using BusinessLayer.Utilities;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class AboutUsPageInfoValidator : AbstractValidator<AboutUsPageInfo>
    {
        private readonly ValidatorMessages _validatorMessages = new ValidatorMessages();

        public AboutUsPageInfoValidator()
        {
            RuleFor(x => x.Header).NotEmpty().WithName("Başlık").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(150)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.Content).NotEmpty().WithName("İçerik").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(1500)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.SeoAuthor).NotEmpty().WithName("Seo Yazar Bilgisi").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(60).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.SeoDescription).NotEmpty().WithName("Seo Açıklama Bilgisi").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.SeoTags).NotEmpty().WithName("Seo Etiket Bilgisi").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);
        }
    }
}
