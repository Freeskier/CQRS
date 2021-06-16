using Application.Functions.PhotoLike.Queries;
using FluentValidation;

namespace Application.Functions.PhotoLike.Validation
{
    public class GetPhotoLikesForPhotoQueryValidation : AbstractValidator<GetPhotoLikesForPhotoQuery>
    {
        public GetPhotoLikesForPhotoQueryValidation()
        {
            RuleFor(x => x.Photo).NotNull();
            RuleFor(x => x.Photo).NotEmpty();
        }
    }
}