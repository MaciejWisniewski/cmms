using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class ServiceCannotBeScheduledForAnAreaRule : IBusinessRule
    {
        private readonly Resource _resource;

        public ServiceCannotBeScheduledForAnAreaRule(Resource resource)
        {
            _resource = resource;
        }

        public bool IsBroken() => _resource.IsArea;

        public string Message => "Service cannot be scheduled for an area";
    }
}
