using Application.Functions.Photo.Queries;
using FluentValidation;

namespace Application.Functions.Photo.Validation
{
    public class GetFolloweesPhotosQueryValidation : AbstractValidator<GetFolloweesPhotosQuery>
    {
        public GetFolloweesPhotosQueryValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}