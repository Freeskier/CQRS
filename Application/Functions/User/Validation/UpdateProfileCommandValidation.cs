using System;
using Application.Functions.User.Commands;
using FluentValidation;

namespace Application.Functions.User.Validation
{
    public class UpdateProfileCommandValidation : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidation()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.User.BirthDate)
                .Must(x => x.Year + 13 <= DateTime.UtcNow.Year);

            RuleFor(x => x.User.Email).EmailAddress();

            RuleFor(x => x.User.UserName).MinimumLength(4).MaximumLength(20);

            RuleFor(x => x.User.City).NotEmpty().NotNull();

            RuleFor(x => x.User.FirstName).NotEmpty().NotNull();
            
            RuleFor(x => x.User.LastName).NotEmpty().NotNull();

        }
    }
}