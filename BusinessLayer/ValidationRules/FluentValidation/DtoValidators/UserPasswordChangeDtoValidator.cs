using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class UserPasswordChangeDtoValidator:AbstractValidator<UserPasswordChangeDto>
    {
        private readonly ValidatorMessage _validatorMessage = new ValidatorMessage();

        public UserPasswordChangeDtoValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().WithName("Şu Anki Şifreniz").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(30).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.NewPassword).NotEmpty().WithName("Yeni Şifreniz").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(30).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.RepeatPassword).NotEmpty().WithName("Yeni Şifrenizin Tekrarı").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(30).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller).Equal(x=>x.NewPassword).WithMessage("Girmiş olduğunuz Yeni Şifreniz ile {PropertyName} birbiri ile uyuşmalıdır.");
        }
    }
}
