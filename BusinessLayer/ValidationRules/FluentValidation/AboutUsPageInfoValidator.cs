using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class AboutUsPageInfoValidator : AbstractValidator<AboutUsPageInfo>
    {
        private readonly ValidatorMessage _validatorMessage = new ValidatorMessage();

        public AboutUsPageInfoValidator()
        {
            RuleFor(x => x.Header).NotEmpty().WithName("Başlık").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(150)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.Content).NotEmpty().WithName("İçerik").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(1500)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.SeoAuthor).NotEmpty().WithName("Seo Yazar Bilgisi").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(60).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.SeoDescription).NotEmpty().WithName("Seo Açıklama Bilgisi").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.SeoTags).NotEmpty().WithName("Seo Etiket Bilgisi").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);
        }
    }
}
