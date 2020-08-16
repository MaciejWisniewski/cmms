using FluentValidation;

namespace CMMS.Application.Maintenance.Failures.FinishFailureRepair
{
    public class FinishFailureRepairCommandValidator : AbstractValidator<FinishFailureRepairCommand>
    {
        public FinishFailureRepairCommandValidator()
        {
            RuleFor(x => x.FailureId).NotEmpty().WithMessage("Failure id cannot be empty");
            RuleFor(x => x.WorkerId).NotEmpty().WithMessage("Worker id cannot be empty");
            RuleFor(x => x.Note).NotEmpty().WithMessage("Note cannot be empty");
        }
    }
}
