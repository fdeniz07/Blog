using BusinessLayer.Utilities;
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class CommentUpdateDtoValidator:AbstractValidator<CommentUpdateDto>
    {
        private readonly ValidatorMessages _validatorMessages = new ValidatorMessages();

        public CommentUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();


            RuleFor(x => x.Content).NotEmpty().WithName("Yorum").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty).MaximumLength(1000)
                .WithMessage("{PropertyName} {MaxLength}" + _validatorMessages.NotBigger).MinimumLength(2).WithMessage("{PropertyName} {MinLength}" + _validatorMessages.NotSmaller);


            RuleFor(x => x.IsActive).NotEmpty().WithName("Aktif Mi?").WithMessage("{PropertyName}" + _validatorMessages.NotEmpty);

            RuleFor(x => x.BlogId).NotEmpty();
        }
    }
}
