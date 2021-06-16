using Application.Functions.Comment.Commands;
using FluentValidation;

namespace Application.Functions.Comment.Validation
{
    public class AddCommentCommandValidation : AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidation()
        {
            RuleFor(x => x.Comment).NotNull();
            
            RuleFor(x => x.Comment.Content)
                .NotNull()
                .NotEmpty()
                .MaximumLength(400)
                .MinimumLength(1);

            RuleFor(x => x.Comment.PhotoId).NotEmpty();
            RuleFor(x => x.LoggedUserId).NotEmpty();
        }
    }
}