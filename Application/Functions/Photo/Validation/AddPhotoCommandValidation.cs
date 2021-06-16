using Application.Functions.Photo.Commands;
using FluentValidation;

namespace Application.Functions.Photo.Validation
{
    public class AddPhotoCommandValidation : AbstractValidator<AddPhotoCommand>
    {
        public AddPhotoCommandValidation()
        {
            RuleFor(x => x.Photo).NotNull();
            RuleFor(x => x.Photo.File).NotNull();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}