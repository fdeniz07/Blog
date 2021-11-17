using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation.DtoValidators
{
    public class CommentAddDtoValidator:AbstractValidator<CommentAddDto>
    {

        public string NotEmptyMessage { get; } = " alanı boş geçilmemelidir.";

        public CommentAddDtoValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty();


            RuleFor(x => x.Content).NotEmpty().WithMessage("Yorum" + NotEmptyMessage).MaximumLength(1000)
                .WithMessage("Yorum alanı 1000 karakterden büyük olmamalıdır.").MinimumLength(2).WithMessage("Yorum alanı 2 karakterden küçük olmamalıdır.");


            RuleFor(x => x.CreatedByName).NotEmpty().WithMessage("Adınız" + NotEmptyMessage).MaximumLength(50).WithMessage("Adınız alanı 50 karakterden büyük olmamalıdır.").MinimumLength(2).WithMessage("Adınız alanı 2 karakterden küçük olmamalıdır.");
        }
    }
}
