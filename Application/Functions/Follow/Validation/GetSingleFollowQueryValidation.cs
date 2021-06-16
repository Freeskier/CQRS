using Application.Functions.Follow.Queries;
using FluentValidation;

namespace Application.Functions.Follow.Validation
{
    public class GetSingleFollowQueryValidation : AbstractValidator<GetSingleFollowQuery>
    {
        public GetSingleFollowQueryValidation()
        {   
            RuleFor(x => x.Follow).NotNull();
            RuleFor(x => x.Follow.FolloweeId).NotEmpty();
            RuleFor(x => x.Follow.FollowerId).NotEmpty();
        }    
    }
}