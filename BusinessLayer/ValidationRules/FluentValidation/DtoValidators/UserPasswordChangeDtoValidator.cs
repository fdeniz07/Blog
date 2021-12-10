using BusinessLayer.Utilities;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class UserPasswordChangeDtoValidator:AbstractValidator<UserPasswordChangeDto>
    {
        private readonly ValidatorMessages _validatorMessages = new ValidatorMessages();

        public UserPasswordChangeDtoValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().WithName("Şu Anki Şifreniz").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(30).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.NewPassword).NotEmpty().WithName("Yeni Şifreniz").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(30).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.RepeatPassword).NotEmpty().WithName("Yeni Şifrenizin Tekrarı").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(30).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller).Equal(x=>x.NewPassword).WithMessage("Girmiş olduğunuz Yeni Şifreniz ile {PropertyName} birbiri ile uyuşmalıdır.");
        }
    }
}
