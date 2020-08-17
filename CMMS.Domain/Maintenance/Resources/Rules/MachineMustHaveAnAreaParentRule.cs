using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Rules
{
    public class MachineMustHaveAnAreaParentRule : IBusinessRule
    {
        private readonly bool _isResourceAMachine;
        private readonly Resource _parent;

        public MachineMustHaveAnAreaParentRule(bool isResourceAMachine, Resource parent)
        {
            _isResourceAMachine = isResourceAMachine;
            _parent = parent;
        }

        public bool IsBroken() => _isResourceAMachine && (_parent == null || !_parent.IsArea);

        public string Message => "Machine must have an area parent";

    }
}
