using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class BlogUpdateDtoValidator : AbstractValidator<BlogUpdateDto>
    {
        private readonly  ValidatorMessage _validatorMessage = new ValidatorMessage();

        public BlogUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(_validatorMessage.NotEmpty);


            RuleFor(x => x.Title).NotEmpty().WithName("Başlık").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.Content).NotEmpty().WithName("İçerik").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.Thumbnail).NotEmpty().WithName("Thumbnail").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(250)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength} " + _validatorMessage.NotSmaller);


            RuleFor(x => x.Image).MaximumLength(100).WithName("Resim").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger);

            RuleFor(x => x.Date).NotEmpty().WithName("Tarih").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty);


            RuleFor(x => x.SeoAuthor).NotEmpty().WithName("Seo Yazar Bilgisi").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(0).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.SeoDescription).NotEmpty().WithName("Seo Açıklama Bilgisi").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(150)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(0).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.SeoTags).NotEmpty().WithName("Seo Etiket Bilgisi").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(0).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.CategoryId).NotEmpty().WithName("Kategori").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty);

            RuleFor(x => x.IsActive).NotEmpty().WithName("Aktif Mi?").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty);

            RuleFor(x => x.IsDelete).NotEmpty().WithName("Silindi Mi?").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty);

            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
