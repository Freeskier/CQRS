using Application.Functions.User.Queries;
using FluentValidation;

namespace Application.Functions.User.Validation
{
    public class GetSingleUserQueryValidation : AbstractValidator<GetSingleUserQuery>
    {
        public GetSingleUserQueryValidation()
        {
            RuleFor(x => x.User).NotNull();
            RuleFor(x => x.User.Id).NotEmpty();
        }
    }
}