using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class CommentAddDtoValidator:AbstractValidator<CommentAddDto>
    {
        private readonly ValidatorMessage _validatorMessage = new ValidatorMessage();

        public CommentAddDtoValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty();


            RuleFor(x => x.Content).NotEmpty().WithName("Yorum").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(1000)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(2).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.CreatedByName).NotEmpty().WithName("Adınız").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(50).WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(2).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);
        }
    }
}
