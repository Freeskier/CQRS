using Application.Functions.Follow.Queries;
using FluentValidation;

namespace Application.Functions.Follow.Validation
{
    public class GetFolloweesQueryValidation : AbstractValidator<GetFolloweesQuery>
    {
        public GetFolloweesQueryValidation()
        {
            RuleFor(x => x.User).NotNull();
        }
    }
}