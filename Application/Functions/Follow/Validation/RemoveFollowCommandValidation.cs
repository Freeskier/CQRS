using Application.Functions.Follow.Commands;
using FluentValidation;

namespace Application.Functions.Follow.Validation
{
    public class RemoveFollowCommandValidation : AbstractValidator<RemoveFollowCommand>
    {
        public RemoveFollowCommandValidation()
        {
            RuleFor(x => x.Follow).NotNull();
            RuleFor(x => x.Follow.FolloweeId).NotEmpty();
            RuleFor(x => x.Follow.FollowerId).NotEmpty();
        }
    }
}