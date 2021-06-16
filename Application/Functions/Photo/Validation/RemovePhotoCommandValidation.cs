using Application.Functions.Photo.Commands;
using FluentValidation;

namespace Application.Functions.Photo.Validation
{
    public class RemovePhotoCommandValidation : AbstractValidator<RemovePhotoCommand>
    {
        public RemovePhotoCommandValidation()
        {
            RuleFor(x => x.LoggedUserId).NotEmpty();
            RuleFor(x => x.PhotoId).NotEmpty();
        }
    }
}