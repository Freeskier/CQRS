using Application.Functions.PhotoLike.Commands;
using FluentValidation;

namespace Application.Functions.PhotoLike.Validation
{
    public class AddPhotoLikeCommandValidation : AbstractValidator<AddPhotoLikeCommand>
    {
        public AddPhotoLikeCommandValidation()
        {
            RuleFor(x => x.Like.PhotoId).NotEmpty();
            RuleFor(x => x.Like.UserId).NotEmpty();
        }
    }
}