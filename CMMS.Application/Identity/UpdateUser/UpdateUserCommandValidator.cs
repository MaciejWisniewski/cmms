using CMMS.Application.Configuration.Validation;
using FluentValidation;

namespace CMMS.Application.Identity.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {

        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName cannot be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber cannot be empty");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Email address is not valid");
            RuleFor(x => x.PhoneNumber).PhoneNumber();
        }
    }
}
