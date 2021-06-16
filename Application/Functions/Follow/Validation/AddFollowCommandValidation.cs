using Application.Functions.Follow.Commands;
using FluentValidation;

namespace Application.Functions.Follow.Validation
{
    public class AddFollowCommandValidation : AbstractValidator<AddFollowCommand>
    {
        public AddFollowCommandValidation()
        {  
            RuleFor(x => x.Follow).NotNull();
            RuleFor(x => x.Follow.FolloweeId).NotEmpty();
            RuleFor(x => x.Follow.FollowerId).NotEmpty();
        }
    }
}