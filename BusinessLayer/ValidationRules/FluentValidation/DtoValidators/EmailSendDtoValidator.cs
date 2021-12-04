using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class EmailSendDtoValidator : AbstractValidator<EmailSendDto>
    {
        private readonly ValidatorMessage _validatorMessage = new ValidatorMessage();

        public EmailSendDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("İsminiz").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(60)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);

            RuleFor(x => x.Email).NotEmpty().WithName("E-Posta Adresiniz").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).EmailAddress().WithMessage("Girilen E-Posta adresi"+_validatorMessage.ValidFormat).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(10).WithMessage("PropertyName} {MinLength}" + _validatorMessage.NotSmaller);

            RuleFor(x => x.Subject).NotEmpty().WithName("Yorum").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(125)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);

            RuleFor(x => x.Message).NotEmpty().WithName("Mesajınız").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(1500)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);
        }
    }
}
