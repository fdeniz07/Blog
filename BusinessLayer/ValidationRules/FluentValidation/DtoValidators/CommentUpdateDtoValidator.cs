using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class CommentUpdateDtoValidator:AbstractValidator<CommentUpdateDto>
    {
        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        public CommentUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();


            RuleFor(x => x.Content).NotEmpty().WithMessage("Yorum" + NotEmptyMessage).MaximumLength(1000)
                .WithMessage("Yorum alanı 1000 karakterden büyük olmamalıdır.").MinimumLength(2).WithMessage("Yorum alanı 2 karakterden küçük olmamalıdır.");


            RuleFor(x => x.IsActive).NotEmpty().WithMessage("Aktif Mi?" + NotEmptyMessage);

            RuleFor(x => x.BlogId).NotEmpty();
        }
    }
}
