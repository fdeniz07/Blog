using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class SmtpSettingsValidator : AbstractValidator<SmtpSettings>
    {
        private readonly ValidatorMessage _validatorMessage = new ValidatorMessage();

        public SmtpSettingsValidator()
        {
            RuleFor(x => x.Server).NotEmpty().WithName("Sunucu").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(100)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.Port).NotEmpty().WithName("Port").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).InclusiveBetween(0, 65535).WithMessage("{PropertyName} 0 ile 65535 arasında olmamalıdır.");


            RuleFor(x => x.SenderName).NotEmpty().WithName("Gönderen Adı").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(100)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(2).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.SenderEmail).EmailAddress().WithName("E-Posta Adresi").WithMessage("Gönderen {PropertyName} uygun formatta olmalıdır.").NotEmpty().WithMessage("Gönderen E-Posta Adresi" + _validatorMessage.NotEmpty).MaximumLength(100).WithMessage("Gönderen {PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(10).WithMessage("Gönderen {PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.Username).EmailAddress().WithName("E-Posta Adresi").WithMessage("Girilen {PropertyName} uygun formatta olmalıdır.").NotEmpty().WithMessage("Kullanıcı Adı/E-Posta" + _validatorMessage.NotEmpty).MaximumLength(100).WithMessage("Kullanıcı Adı/{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(1).WithMessage("Kullanıcı Adı/{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.Password).NotEmpty().WithName("Şifre").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


        }
    }

}
