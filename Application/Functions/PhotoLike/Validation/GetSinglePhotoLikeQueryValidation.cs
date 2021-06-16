using Application.Functions.PhotoLike.Queries;
using FluentValidation;

namespace Application.Functions.PhotoLike.Validation
{
    public class GetSinglePhotoLikeQueryValidation : AbstractValidator<GetSinglePhotoLikeQuery>
    {   
        public GetSinglePhotoLikeQueryValidation()
        {   
            RuleFor(x => x.Like).NotNull();
            RuleFor(x => x.Like.PhotoId).NotEmpty();
            RuleFor(x => x.Like.UserId).NotEmpty();
        }
    }
}