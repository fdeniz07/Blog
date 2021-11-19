using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class UserPasswordChangeDtoValidator:AbstractValidator<UserPasswordChangeDto>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        public UserPasswordChangeDtoValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Şu Anki Şifreniz" + NotEmptyMessage).MaximumLength(30)
                .WithMessage("Şu Anki Şifreniz alanı 30 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Şu Anki Şifreniz alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Yeni Şifreniz" + NotEmptyMessage).MaximumLength(30)
                .WithMessage("Yeni Şifreniz alanı 30 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Yeni Şifreniz alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.RepeatPassword).NotEmpty().WithMessage("Yeni Şifrenizin Tekrarı" + NotEmptyMessage).MaximumLength(30)
                .WithMessage("Yeni Şifrenizin Tekrarı alanı 30 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Yeni Şifrenizin Tekrarı alanı 5 karakterden küçük olmamalıdır.").Equal(x=>x.NewPassword).WithMessage("Girmiş olduğunuz Yeni Şifreniz ile Yeni Şifrenizin Tekrarı alanları birbiri ile uyuşmalıdır.");
        }
    }
}
