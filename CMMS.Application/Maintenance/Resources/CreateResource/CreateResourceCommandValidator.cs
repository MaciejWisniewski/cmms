using FluentValidation;

namespace CMMS.Application.Maintenance.Resources.CreateResource
{
    public class CreateResourceCommandValidator : AbstractValidator<CreateResourceCommand>
    {
        public CreateResourceCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Resource name cannot be empty");
        }
    }
}
