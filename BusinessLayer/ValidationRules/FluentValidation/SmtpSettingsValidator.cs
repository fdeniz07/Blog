using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class SmtpSettingsValidator : AbstractValidator<SmtpSettings>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        public SmtpSettingsValidator()
        {
            RuleFor(x => x.Server).NotEmpty().WithMessage("Sunucu" + NotEmptyMessage).MaximumLength(100)
                .WithMessage("Sunucu alanı 100 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Sunucu alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Port).NotEmpty().WithMessage("Port" + NotEmptyMessage).InclusiveBetween(0, 65535).WithMessage("Port alanı 0 ile 65535 arasında olmamalıdır.");


            RuleFor(x => x.SenderName).NotEmpty().WithMessage("Gönderen Adı" + NotEmptyMessage).MaximumLength(100)
                .WithMessage("Gönderen Adı alanı 100 karakterden büyük olmamalıdır.").MinimumLength(2).WithMessage("Gönderen Adı alanı 2 karakterden küçük olmamalıdır.");


            RuleFor(x => x.SenderEmail).EmailAddress().WithMessage("Girilen E-Posta adresi uygun formatta olmalıdır.").NotEmpty().WithMessage("Gönderen E-Posta Adresi" + NotEmptyMessage).MaximumLength(100).WithMessage("Gönderen E-Posta Adresi alanı 100 karakterden büyük olmamalıdır.").MinimumLength(10).WithMessage("Gönderen E-Posta Adresi alanı 10 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Username).EmailAddress().WithMessage("Girilen E-Posta adresi uygun formatta olmalıdır.").NotEmpty().WithMessage("Kullanıcı Adı/E-Posta" + NotEmptyMessage).MaximumLength(100).WithMessage("Kullanıcı Adı/E-Posta alanı 100 karakterden büyük olmamalıdır.").MinimumLength(1).WithMessage("Kullanıcı Adı/E-Posta alanı 1 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre" + NotEmptyMessage).MaximumLength(50)
                .WithMessage("Şifre alanı 50 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Şifre alanı 5 karakterden küçük olmamalıdır.");


        }
    }

}
