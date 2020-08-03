using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class WorkerMustHaveAccessToTheResourceToManageItsServicesRule : IBusinessRule
    {
        private readonly List<ResourceAccess> _resourceAccesses;
        private readonly WorkerId _workerId;

        public WorkerMustHaveAccessToTheResourceToManageItsServicesRule(List<ResourceAccess> resourceAccesses, WorkerId workerId)
        {
            _resourceAccesses = resourceAccesses;
            _workerId = workerId;
        }

        public bool IsBroken() => _resourceAccesses.FirstOrDefault(a => a.WorkerId == _workerId) == null;

        public string Message => "Worker must have access to the resource to manage its services";
    }
}
