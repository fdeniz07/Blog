using BusinessLayer.Utilities;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class EmailSendDtoValidator : AbstractValidator<EmailSendDto>
    {
        private readonly ValidatorMessages _validatorMessages = new ValidatorMessages();

        public EmailSendDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("İsminiz").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(60)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);

            RuleFor(x => x.Email).NotEmpty().WithName("E-Posta Adresiniz").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).EmailAddress().WithMessage("Girilen E-Posta adresi"+_validatorMessages.ValidFormat).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(10).WithMessage("PropertyName} {MinLength}" + _validatorMessages.NotSmaller);

            RuleFor(x => x.Subject).NotEmpty().WithName("Yorum").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(125)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);

            RuleFor(x => x.Message).NotEmpty().WithName("Mesajınız").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(1500)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);
        }
    }
}
