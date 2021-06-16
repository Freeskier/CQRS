using Domain.Dtos;
using FluentValidation;

namespace Application.SignalR.Validation
{
    public class MessageRequestValidation : AbstractValidator<MessageRequest>
    {
        public MessageRequestValidation()
        {
            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x)
                .Must(x => x.ReceiverId != x.SenderId)
                .WithMessage("You cannot send message to yourself.");
        }
    }
}