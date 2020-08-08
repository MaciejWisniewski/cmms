using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class AlreadyStartedOrFinishedServiceCannotBeEditedRule : IBusinessRule
    {
        private readonly DateTime? _actualStartDateTime;
        private readonly DateTime? _actualEndDateTime;

        public AlreadyStartedOrFinishedServiceCannotBeEditedRule(DateTime? actualStartDateTime, DateTime? actualEndDateTime)
        {
            _actualStartDateTime = actualStartDateTime;
            _actualEndDateTime = actualEndDateTime;
        }

        public bool IsBroken() => _actualStartDateTime != null || _actualEndDateTime != null;

        public string Message => "Already started or finished service cannot be edited";
    }
}
