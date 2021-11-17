using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class CategoryAddDtoValidator : AbstractValidator<CategoryAddDto>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";


        public CategoryAddDtoValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori Adı" + NotEmptyMessage).MaximumLength(50)
                .WithMessage("Kategori Adı alanı 50 karakterden büyük olmamalıdır.").MinimumLength(2).WithMessage("Kategori Adı alanı 2 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Description).MaximumLength(150).WithMessage("Kategori Açıklaması alanı 150 karakterden büyük olmamalıdır.").MinimumLength(3).WithMessage("Kategori Açıklaması alanı 3 karakterden küçük olmamalıdır.");


            RuleFor(x => x.Note).MaximumLength(500).WithMessage("Kategori Özel Not alanı 500 karakterden büyük olmamalıdır.").MinimumLength(3).WithMessage("Kategori Özel Not alanı 3 karakterden küçük olmamalıdır.");


            RuleFor(x => x.IsActive).NotEmpty().WithMessage("Aktif Mi?" + NotEmptyMessage);
        }
    }
}
