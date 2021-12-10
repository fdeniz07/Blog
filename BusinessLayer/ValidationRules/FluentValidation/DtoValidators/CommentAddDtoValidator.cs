using BusinessLayer.Utilities;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class CommentAddDtoValidator:AbstractValidator<CommentAddDto>
    {
        private readonly ValidatorMessages _validatorMessages = new ValidatorMessages();

        public CommentAddDtoValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty();


            RuleFor(x => x.Content).NotEmpty().WithName("Yorum").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(1000)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(2).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.CreatedByName).NotEmpty().WithName("Adınız").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(50).WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(2).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);
        }
    }
}
