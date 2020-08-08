using FluentValidation;

namespace CMMS.Application.Maintenance.Services.EditScheduledService
{
    public class EditScheduledServiceCommandValidator : AbstractValidator<EditScheduledServiceCommand>
    {
        public EditScheduledServiceCommandValidator()
        {
            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("Service id cannot be empty");
            RuleFor(x => x.ResourceId).NotEmpty().WithMessage("Resource id cannot be empty");
            RuleFor(x => x.ServiceTypeId).NotEmpty().WithMessage("Service type id cannot be empty");
            RuleFor(x => x.ScheduledWorkerId).NotEmpty().WithMessage("Scheduled worker id cannot be empty");
            RuleFor(x => x.ScheduledStartDateTime).NotEmpty().WithMessage("Scheduled start date-time cannot be empty");
            RuleFor(x => x.ScheduledEndDateTime).NotEmpty().WithMessage("Scheduled end date-time cannot be empty");
        }
    }
}
