using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class OnlyActualWorkerCanFinishServiceRule : IBusinessRule
    {
        private readonly WorkerId _actualWorkerId;
        private readonly WorkerId _finishingWorkerId;

        public OnlyActualWorkerCanFinishServiceRule(WorkerId actualWorkerId, WorkerId finishingWorkerId)
        {
            _actualWorkerId = actualWorkerId;
            _finishingWorkerId = finishingWorkerId;
        }

        public bool IsBroken() => _finishingWorkerId != _actualWorkerId;

        public string Message => "Only worker who started service can finish it";
    }
}
