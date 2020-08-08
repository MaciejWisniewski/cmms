using FluentValidation;

namespace CMMS.Application.Maintenance.Services.RemoveScheduledService
{
    public class RemoveScheduledServiceCommandValidator : AbstractValidator<RemoveScheduledServiceCommand>
    {
        public RemoveScheduledServiceCommandValidator()
        {
            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("Service id cannot be empty");
        }
    }
}
