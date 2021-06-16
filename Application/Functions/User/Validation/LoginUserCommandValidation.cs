using Application.Functions.User.Commands;
using FluentValidation;

namespace Application.Functions.User.Validation
{
    public class LoginUserCommandValidation : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidation()
        {
            RuleFor(x => x.User.UserName).NotNull().NotEmpty();
            
            RuleFor(x => x.User.Password).NotNull().NotEmpty();
        }
    }
}