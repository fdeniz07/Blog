using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class CategoryUpdateDtoValidator:AbstractValidator<CategoryUpdateDto>
    {
        private readonly ValidatorMessage _validatorMessage = new ValidatorMessage();

        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();


            RuleFor(x => x.CategoryName).NotEmpty().WithName("Kategori Adı").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(2).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.Description).MaximumLength(150).WithName("Kategori Açıklaması").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(3).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.Note).MaximumLength(500).WithName("Kategori Özel Not").WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(3).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.IsActive).NotEmpty().WithName("Aktif Mi?").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty);

            RuleFor(x => x.IsDeleted).NotEmpty().WithName("Silindi Mi?").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty);
        }
    }
}
