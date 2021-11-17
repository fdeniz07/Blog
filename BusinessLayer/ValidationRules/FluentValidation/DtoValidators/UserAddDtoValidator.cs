using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class UserAddDtoValidator:AbstractValidator<UserAddDto>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        public UserAddDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı" + NotEmptyMessage).MaximumLength(50)
                .WithMessage("Kullanıcı Adı alanı 50 karakterden büyük olmamalıdır.").MinimumLength(3).WithMessage("Kullanıcı Adı alanı 3 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta Adresi" + NotEmptyMessage).EmailAddress().WithMessage("Girilen E-Posta adresi uygun formatta olmalıdır.").MaximumLength(100).WithMessage("E-Posta Adresi alanı 100 karakterden büyük olmamalıdır.").MinimumLength(10).WithMessage("E-Posta Adresi alanı 10 karakterden küçük olmamalıdır.");



            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre" + NotEmptyMessage).MaximumLength(30)
                .WithMessage("Şifre alanı 30 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Şifre alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası" + NotEmptyMessage).MaximumLength(14)
                .WithMessage("Telefon Numarası alanı 14 karakterden büyük olmamalıdır.").MinimumLength(12).WithMessage("Telefon Numarası alanı 12 karakterden küçük olmamalıdır.");


            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Lütfen bir resim seçiniz");


            RuleFor(x => x.FirstName).MaximumLength(30).WithMessage("Adı alanı 30 karakterden büyük olmamalıdır.").MinimumLength(2).WithMessage("Adı alanı 2 karakterden küçük olmamalıdır.");


            RuleFor(x => x.LastName).MaximumLength(30).WithMessage("Soyadı alanı 30 karakterden büyük olmamalıdır.").MinimumLength(2).WithMessage("Soyadı alanı 2 karakterden küçük olmamalıdır.");


            RuleFor(x => x.About).MaximumLength(1000).WithMessage("Kullanıcı Adı alanı 1000 karakterden büyük olmamalıdır.").MinimumLength(5).WithMessage("Kullanıcı Adı alanı 5 karakterden küçük olmamalıdır.");


            RuleFor(x => x.TwitterLink).MaximumLength(250).WithMessage("Twitter alanı 250 karakterden büyük olmamalıdır.").MinimumLength(20).WithMessage("Kullanıcı Adı alanı 20 karakterden küçük olmamalıdır.");


            RuleFor(x => x.TwitterLink).MaximumLength(250).WithMessage("Twitter alanı 250 karakterden büyük olmamalıdır.").MinimumLength(20).WithMessage("Kullanıcı Adı alanı 20 karakterden küçük olmamalıdır.");


            RuleFor(x => x.TwitterLink).MaximumLength(250).WithMessage("Twitter alanı 250 karakterden büyük olmamalıdır.").MinimumLength(20).WithMessage("Twitter alanı 20 karakterden küçük olmamalıdır.");


            RuleFor(x => x.FacebookLink).MaximumLength(250).WithMessage("Facebook alanı 250 karakterden büyük olmamalıdır.").MinimumLength(20).WithMessage("Facebook alanı 20 karakterden küçük olmamalıdır.");


            RuleFor(x => x.InstagramLink).MaximumLength(250).WithMessage("Instagram alanı 250 karakterden büyük olmamalıdır.").MinimumLength(20).WithMessage("Instagram alanı 20 karakterden küçük olmamalıdır.");


            RuleFor(x => x.LinkedInLink).MaximumLength(250).WithMessage("LinkedIn alanı 250 karakterden büyük olmamalıdır.").MinimumLength(20).WithMessage("LinkedIn alanı 20 karakterden küçük olmamalıdır.");


            RuleFor(x => x.YoutubeLink).MaximumLength(250).WithMessage("Youtube alanı 250 karakterden büyük olmamalıdır.").MinimumLength(20).WithMessage("Youtube alanı 20 karakterden küçük olmamalıdır.");


            RuleFor(x => x.GitHubLink).MaximumLength(250).WithMessage("GitHub alanı 250 karakterden büyük olmamalıdır.").MinimumLength(20).WithMessage("GitHub alanı 20 karakterden küçük olmamalıdır.");


            RuleFor(x => x.WebsiteLink).MaximumLength(250).WithMessage("Website alanı 250 karakterden büyük olmamalıdır.").MinimumLength(20).WithMessage("Website alanı 20 karakterden küçük olmamalıdır.");
        }
    }
}
