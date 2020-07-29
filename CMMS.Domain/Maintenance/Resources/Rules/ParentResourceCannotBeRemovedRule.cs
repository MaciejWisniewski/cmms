using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Rules
{
    public class ParentResourceCannotBeRemovedRule : IBusinessRule
    {
        private readonly Resource _resource;

        public ParentResourceCannotBeRemovedRule(Resource resource)
        {
            _resource = resource;
        }

        public bool IsBroken() => _resource.Children.Count > 0;

        public string Message => "Parent resource cannot be removed";
    }
}
