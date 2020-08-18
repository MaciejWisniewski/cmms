using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class ServiceCanBeStartedOnceRule : IBusinessRule
    {
        private readonly DateTime? _actualStartDateTime;

        public ServiceCanBeStartedOnceRule(DateTime? actualStartDateTime)
        {
            _actualStartDateTime = actualStartDateTime;
        }

        public bool IsBroken() => _actualStartDateTime != null;

        public string Message => "Service has already started";
    }
}
