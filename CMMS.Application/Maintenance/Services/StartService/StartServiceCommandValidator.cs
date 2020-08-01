using FluentValidation;

namespace CMMS.Application.Maintenance.Services.StartService
{
    public class StartServiceCommandValidator : AbstractValidator<StartServiceCommand>
    {
        public StartServiceCommandValidator()
        {
            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("Service id cannot be empty");
            RuleFor(x => x.ActualWorkerId).NotEmpty().WithMessage("Worker id cannot be empty");
            RuleFor(x => x.Note).NotEmpty().WithMessage("Note cannot be empty");
        }
    }
}
