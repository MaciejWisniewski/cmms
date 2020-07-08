using CMMS.Application.Configuration.Validation;
using FluentValidation;

namespace CMMS.Application.Identity.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name cannot be empty");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number cannot be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Email address is not valid");
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.Password).Password();
        }
    }
}
