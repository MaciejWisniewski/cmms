using FluentValidation;

namespace CMMS.Application.Maintenance.Resources.RemoveResource
{
    public class RemoveResourceCommandValidator : AbstractValidator<RemoveResourceCommand>
    {
        public RemoveResourceCommandValidator()
        {
            RuleFor(x => x.ResourceId).NotEmpty().WithMessage("ResourceId cannot be empty");
        }
    }
}
