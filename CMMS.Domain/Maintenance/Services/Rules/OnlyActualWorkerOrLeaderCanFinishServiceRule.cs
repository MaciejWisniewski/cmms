using CMMS.Domain.Identity;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class OnlyActualWorkerOrLeaderCanFinishServiceRule : IBusinessRule
    {
        private readonly WorkerId _actualWorkerId;
        private readonly Worker _finishingWorker;

        public OnlyActualWorkerOrLeaderCanFinishServiceRule(WorkerId actualWorkerId, Worker finishingWorker)
        {
            _actualWorkerId = actualWorkerId;
            _finishingWorker = finishingWorker;
        }

        public bool IsBroken() => _finishingWorker.Id != _actualWorkerId && _finishingWorker.Role != UserRole.Leader;

        public string Message => "Only worker who started service or leader can finish it";
    }
}
