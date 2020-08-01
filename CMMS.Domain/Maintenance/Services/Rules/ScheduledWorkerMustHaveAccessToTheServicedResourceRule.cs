using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class ScheduledWorkerMustHaveAccessToTheServicedResourceRule : IBusinessRule
    {
        private readonly List<ResourceAccess> _resourceAccesses;
        private readonly WorkerId _scheduledWorkerId;

        public ScheduledWorkerMustHaveAccessToTheServicedResourceRule(
            List<ResourceAccess> resourceAccesses,
            WorkerId scheduledWorkerId)
        {
            _resourceAccesses = resourceAccesses;
            _scheduledWorkerId = scheduledWorkerId;
        }

        public bool IsBroken() => _resourceAccesses.FirstOrDefault(a => a.WorkerId == _scheduledWorkerId) == null;

        public string Message => "Scheduled worker doesn't have accesss to the resource";
    }
}
