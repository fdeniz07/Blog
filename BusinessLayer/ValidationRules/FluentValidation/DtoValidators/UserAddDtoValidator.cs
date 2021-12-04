﻿using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class UserAddDtoValidator:AbstractValidator<UserAddDto>
    {
        private readonly ValidatorMessage _validatorMessage = new ValidatorMessage();

        public UserAddDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithName("Kullanıcı Adı").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(3).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.Email).NotEmpty().WithName("E-Posta Adresi").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).EmailAddress().WithMessage("Girilen E-Posta adresi"+_validatorMessage.ValidFormat).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(10).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);



            RuleFor(x => x.Password).NotEmpty().WithName("Şifre").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(30)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.PhoneNumber).NotEmpty().WithName("Telefon Numarası").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(14)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(12).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.ImageFile).NotEmpty().WithName("Resim Dosyası").WithMessage("Lütfen bir resim seçiniz");


            RuleFor(x => x.FirstName).MaximumLength(30).WithName("İsminiz").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(2).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.LastName).MaximumLength(30).WithName("Soyadız").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(2).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.About).MaximumLength(1000).WithName("Hakkında").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.TwitterLink).MaximumLength(250).WithName("Twitter").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);

            

            RuleFor(x => x.FacebookLink).MaximumLength(250).WithName("Facebook").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.InstagramLink).MaximumLength(250).WithName("Instagram").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.LinkedInLink).MaximumLength(250).WithName("LinkedIn").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.YoutubeLink).MaximumLength(250).WithName("Youtube").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.GitHubLink).MaximumLength(250).WithName("GitHub").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.WebsiteLink).MaximumLength(250).WithName("Website").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);
        }
    }
}
