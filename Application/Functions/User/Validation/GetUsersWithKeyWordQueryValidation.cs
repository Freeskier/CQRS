using Application.Functions.User.Queries;
using FluentValidation;

namespace Application.Functions.User.Validation
{
    public class GetUsersWithKeyWordQueryValidation : AbstractValidator<GetUsersWithKeyWordQuery>
    {
        public GetUsersWithKeyWordQueryValidation()
        {
            RuleFor(x => x.KeyWord).MinimumLength(3);
        }
    }
}