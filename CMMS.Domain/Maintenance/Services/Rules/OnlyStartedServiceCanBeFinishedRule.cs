using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class OnlyStartedServiceCanBeFinishedRule : IBusinessRule
    {
        private readonly DateTime? _actualStartDateTime;

        public OnlyStartedServiceCanBeFinishedRule(DateTime? actualStartDateTime)
        {
            _actualStartDateTime = actualStartDateTime;
        }

        public bool IsBroken() => _actualStartDateTime == null;

        public string Message => "Cannot finish not-started service";
    }
}
