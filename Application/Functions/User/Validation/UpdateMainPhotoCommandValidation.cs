using Application.Functions.User.Commands;
using FluentValidation;

namespace Application.Functions.User.Validation
{
    public class UpdateMainPhotoCommandValidation : AbstractValidator<UpdateMainPhotoCommand>
    {
        public UpdateMainPhotoCommandValidation()
        {
            RuleFor(x => x.Photo).NotNull();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Photo.File).NotNull();
        }
    }
}