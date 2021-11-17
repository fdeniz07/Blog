using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class EmailSendDtoValidator:AbstractValidator<EmailSendDto>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        public EmailSendDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsminiz" + NotEmptyMessage).MaximumLength(60)
                .WithMessage("İsminiz alanı 60 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("İsminiz alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta Adresiniz" + NotEmptyMessage).EmailAddress().WithMessage("Girilen E-Posta adresi uygun formatta olmalıdır.").MaximumLength(100).WithMessage("E-Posta Adresiniz alanı 100 karakterden büyük olmamalıdır.").MinimumLength(10).WithMessage("E-Posta Adresiniz alanı 10 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu" + NotEmptyMessage).MaximumLength(125)
                .WithMessage("Konu alanı 125 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Yorum alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesajınız" + NotEmptyMessage).MaximumLength(1500)
                .WithMessage("Mesajınız alanı 1500 karakterden büyük olmamalıdır.").MinimumLength(20).WithMessage("Mesajınız alanı 20 karakterden küçük olmamalıdır.");
        }
    }
}
