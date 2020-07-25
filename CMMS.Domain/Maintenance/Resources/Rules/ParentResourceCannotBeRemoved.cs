using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Resources.Rules
{
    public class ParentResourceCannotBeRemoved : IBusinessRule
    {
        private readonly Resource _resource;

        public ParentResourceCannotBeRemoved(Resource resource)
        {
            _resource = resource;
        }

        public bool IsBroken() => _resource.Children.Count > 0;

        public string Message => "Parent resource cannot be removed";
    }
}
