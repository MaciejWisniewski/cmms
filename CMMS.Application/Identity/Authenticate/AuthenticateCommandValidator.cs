using FluentValidation;

namespace CMMS.Application.Identity.Authenticate
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is empty");
        }
    }
}
