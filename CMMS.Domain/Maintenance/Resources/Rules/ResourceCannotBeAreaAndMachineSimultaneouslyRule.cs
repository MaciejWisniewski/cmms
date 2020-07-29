using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Rules
{
    public class ResourceCannotBeAreaAndMachineSimultaneouslyRule : IBusinessRule
    {
        private readonly bool _isArea;
        private readonly bool _isMachine;

        public ResourceCannotBeAreaAndMachineSimultaneouslyRule(bool isArea, bool isMachine)
        {
            _isArea = isArea;
            _isMachine = isMachine;
        }

        public bool IsBroken() => _isArea && _isMachine;

        public string Message => "Resource cannot be an area and machine simultaneously";

    }
}
