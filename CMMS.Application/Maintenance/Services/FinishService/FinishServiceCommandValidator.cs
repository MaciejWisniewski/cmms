using FluentValidation;

namespace CMMS.Application.Maintenance.Services.FinishService
{
    public class FinishServiceCommandValidator : AbstractValidator<FinishServiceCommand>
    {
        public FinishServiceCommandValidator()
        {
            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("Service id cannot be empty");
            RuleFor(x => x.FinishingWorkerId).NotEmpty().WithMessage("Finishing worker id cannot be empty");
            RuleFor(x => x.Note).NotEmpty().WithMessage("Service note must be provided");
        }
    }
}
