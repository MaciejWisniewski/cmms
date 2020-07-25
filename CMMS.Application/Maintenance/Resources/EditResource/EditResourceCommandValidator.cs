using FluentValidation;

namespace CMMS.Application.Maintenance.Resources.EditResource
{
    public class EditResourceCommandValidator : AbstractValidator<EditResourceCommand>
    {
        public EditResourceCommandValidator()
        {
            RuleFor(x => x.ResourceId).NotEmpty().WithMessage("Resource id cannot be empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Resource name cannot be empty");
        }
    }
}
