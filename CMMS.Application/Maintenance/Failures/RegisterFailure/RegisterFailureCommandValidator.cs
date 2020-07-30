using FluentValidation;

namespace CMMS.Application.Maintenance.Failures.RegisterFailure
{
    public class RegisterFailureCommandValidator : AbstractValidator<RegisterFailureCommand>
    {
        public RegisterFailureCommandValidator()
        {
            RuleFor(x => x.ResourceId).NotEmpty().WithMessage("Resource ID cannot be empty");
            RuleFor(x => x.ProblemDescription).NotEmpty().WithMessage("Problem description must be provided");
        }
    }
}
