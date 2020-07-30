using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Rules
{
    public class ParentResourceCannotBeAMachineRule : IBusinessRule
    {
        private readonly Resource _parent;

        public ParentResourceCannotBeAMachineRule(Resource parent)
        {
            _parent = parent;
        }

        public bool IsBroken() => _parent != null && _parent.IsMachine;

        public string Message => "Parent cannot be a machine";
    }
}
