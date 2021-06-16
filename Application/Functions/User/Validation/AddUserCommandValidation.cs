using System;
using Application.Functions.User.Commands;
using FluentValidation;

namespace Application.Functions.User.Validation
{
    public class AddUserCommandValidation : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidation()
        {
            RuleFor(x => x.User.BirthDate)
                .Must(x => x.Year + 13 <= DateTime.UtcNow.Year)
                .WithMessage("You're under 13 years old.");

            RuleFor(x => x.User.Email).EmailAddress();

            RuleFor(x => x.User.UserName).MinimumLength(4).MaximumLength(20);
            
            RuleFor(x => x.User.City).NotEmpty().NotNull().MaximumLength(20);

            RuleFor(x => x.User.FirstName).NotEmpty().NotNull().MaximumLength(20);
            
            RuleFor(x => x.User.LastName).NotEmpty().NotNull().MaximumLength(20);

            RuleFor(x => x.User.Password).MinimumLength(6).MaximumLength(30);

            RuleFor(x => x.User).Must(x => x.Password == x.ConfirmPassword).WithMessage("Passwords don't match.");

            RuleFor(x => x.User).Must(x => x.Email == x.ConfirmEmail).WithMessage("Email addressess don't match.");
        }
    }
}