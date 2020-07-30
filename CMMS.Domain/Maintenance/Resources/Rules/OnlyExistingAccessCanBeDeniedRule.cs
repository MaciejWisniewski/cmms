using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;
using System.Linq;

namespace CMMS.Domain.Maintenance.Resources.Rules
{
    public class OnlyExistingAccessCanBeDeniedRule : IBusinessRule
    {
        private readonly Resource _resource;
        private readonly WorkerId _workerId;

        public OnlyExistingAccessCanBeDeniedRule(Resource resource, WorkerId workerId)
        {
            _resource = resource;
            _workerId = workerId;
        }

        public bool IsBroken() => _resource.Accesses.FirstOrDefault(a => a.WorkerId == _workerId) == null;

        public string Message => "Worker doesn't have access to the given resource";
    }
}
