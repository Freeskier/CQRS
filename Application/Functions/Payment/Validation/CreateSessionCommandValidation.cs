using FluentValidation;
using Application.Functions.Payment.Commands;

namespace Application.Functions.Payment.Validation
{
    public class CreateSessionCommandValidation : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionCommandValidation()
        {
            RuleFor(x => x.Payment).NotNull();
            
            RuleFor(x => x.Payment.Value)
                .GreaterThanOrEqualTo(200)
                .WithMessage("You have to pay at least 2 pln.");
        }
    }
}