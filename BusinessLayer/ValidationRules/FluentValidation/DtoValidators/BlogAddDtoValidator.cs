using BusinessLayer.Utilities;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class BlogAddDtoValidator : AbstractValidator<BlogAddDto>
    {
        private readonly ValidatorMessages _validatorMessages = new ValidatorMessages();

        public BlogAddDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithName("Başlık").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(100)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.Content).NotEmpty().WithName("İçerik").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MinimumLength(20).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.Thumbnail).NotEmpty().WithName("Küçük Resim").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(250)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(5).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.Image).MaximumLength(100).WithName("Resim alanı").WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger);


            RuleFor(x => x.Date).NotEmpty().WithName("Tarih").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);


            RuleFor(x => x.SeoAuthor).NotEmpty().WithName("Seo Yazar Bilgisi").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(0).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.SeoDescription).NotEmpty().WithName("Seo Açıklama Bilgisi").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(150).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(0).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.SeoTags).NotEmpty().WithName("Seo Etiket Bilgisi").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(100).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(0).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.CategoryId).NotEmpty().WithName("Kategori").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);


            RuleFor(x => x.IsActive).NotEmpty().WithName("Aktif Mi?").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

        }
    }
}
