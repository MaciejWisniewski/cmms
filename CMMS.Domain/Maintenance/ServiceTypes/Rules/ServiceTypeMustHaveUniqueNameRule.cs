using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.ServiceTypes.Rules
{
    public class ServiceTypeMustHaveUniqueNameRule : IBusinessRule
    {
        private readonly string _typeName;
        private readonly IServiceTypeUniquenessChecker _uniquenessChecker;

        public ServiceTypeMustHaveUniqueNameRule(string typeName, IServiceTypeUniquenessChecker uniquenessChecker)
        {
            _typeName = typeName;
            _uniquenessChecker = uniquenessChecker;
        }

        public bool IsBroken() => !_uniquenessChecker.IsUnique(_typeName);

        public string Message => "Service type with the given name already exists";

    }
}
