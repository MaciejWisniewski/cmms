using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Rules
{
    public class ParentCannotBeAMachine : IBusinessRule
    {
        private readonly Resource _parent;

        public ParentCannotBeAMachine(Resource parent)
        {
            _parent = parent;
        }

        public bool IsBroken() => _parent != null && _parent.IsMachine;

        public string Message => "Parent cannot be a machine";
    }
}
