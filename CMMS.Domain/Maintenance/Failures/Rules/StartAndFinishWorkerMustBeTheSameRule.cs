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

        public bool IsBroken()
        {
           return  _startWorker != _finishWorker;
        }
        public string Message => "A worker who hasn't started the repair cannot finish it";
    }
}
