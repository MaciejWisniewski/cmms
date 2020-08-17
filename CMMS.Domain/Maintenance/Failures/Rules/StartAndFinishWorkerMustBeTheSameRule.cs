using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Failures.Rules
{
    public class StartAndFinishWorkerMustBeTheSameRule : IBusinessRule
    {
        private readonly WorkerId _startWorker;
        private readonly WorkerId _finishWorker;

        public StartAndFinishWorkerMustBeTheSameRule(WorkerId startWorker, WorkerId finishWorker)
        {
            _startWorker = startWorker;
            _finishWorker = finishWorker;
        }

        public string Message => "Worker which start repair could only finish this action";

        public bool IsBroken()
        {
            return _startWorker != _finishWorker;
        }
    }
}
