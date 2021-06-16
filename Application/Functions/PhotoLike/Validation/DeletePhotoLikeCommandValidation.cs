using Application.Functions.PhotoLike.Commands;
using FluentValidation;

namespace Application.Functions.PhotoLike.Validation
{
    public class DeletePhotoLikeCommandValidation : AbstractValidator<DeletePhotoLikeCommand>
    {
        public DeletePhotoLikeCommandValidation()
        {
            RuleFor(x => x.Like.PhotoId).NotEmpty();
            RuleFor(x => x.Like.UserId).NotEmpty();
        }
    }
}