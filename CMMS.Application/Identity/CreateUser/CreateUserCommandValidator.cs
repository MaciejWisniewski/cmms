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
            RuleFor(x => x.PhoneNumber)
                .Matches("(?<!\\w)(\\(?(\\+|00)?48\\)?)?[ -]?\\d{3}[ -]?\\d{3}[ -]?\\d{3}(?!\\w)")
                .WithMessage("Phone number format is invalid");
            RuleFor(x => x.Password)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$")
                .WithMessage("Password must have at least one uppercase and lowercase character, one digit, one special character and have minimum 8 characters");
        }
    }
}
