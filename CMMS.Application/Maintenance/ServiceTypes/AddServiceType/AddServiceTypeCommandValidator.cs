using FluentValidation;

namespace CMMS.Application.Maintenance.ServiceTypes.AddServiceType
{
    public class AddServiceTypeCommandValidator : AbstractValidator<AddServiceTypeCommand>
    {
        public AddServiceTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Service type name cannot be empty");
            RuleFor(x => x.Name).MaximumLength(80).WithMessage("Service type name maximum length is 80 characters");
        }
    }
}
