using Application.Functions.Follow.Queries;
using FluentValidation;

namespace Application.Functions.Follow.Validation
{
    public class GetFollowersQueryValidation : AbstractValidator<GetFollowersQuery>
    {
        public GetFollowersQueryValidation()
        {
            RuleFor(x => x.User).NotNull();
        }
    }
}