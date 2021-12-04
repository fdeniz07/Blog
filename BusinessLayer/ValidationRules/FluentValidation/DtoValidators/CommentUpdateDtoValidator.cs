using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class CommentUpdateDtoValidator:AbstractValidator<CommentUpdateDto>
    {
        private readonly ValidatorMessage _validatorMessage = new ValidatorMessage();

        public CommentUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();


            RuleFor(x => x.Content).NotEmpty().WithName("Yorum").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty).MaximumLength(1000)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessage.NotBigger).MinimumLength(2).WithMessage("{PropertyName} {MinLength}" + _validatorMessage.NotSmaller);


            RuleFor(x => x.IsActive).NotEmpty().WithName("Aktif Mi?").WithMessage("{PropertyName}" + _validatorMessage.NotEmpty);

            RuleFor(x => x.BlogId).NotEmpty();
        }
    }
}
