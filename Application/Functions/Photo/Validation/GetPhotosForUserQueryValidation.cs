using Application.Functions.Photo.Queries;
using FluentValidation;

namespace Application.Functions.Photo.Validation
{
    public class GetPhotosForUserQueryValidation : AbstractValidator<GetPhotosForUserQuery>
    {
        public GetPhotosForUserQueryValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}