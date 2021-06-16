using Application.Functions.Comment.Commands;
using FluentValidation;

namespace Application.Functions.Comment.Validation
{
    public class RemoveCommentCommandValidation : AbstractValidator<RemoveCommentCommand>
    {
        public RemoveCommentCommandValidation()
        {
            RuleFor(x => x.Comment).NotNull();
            RuleFor(x => x.Comment.Id).NotEmpty();
            RuleFor(x => x.LoggedUserId).NotEmpty();
        }
    }
}