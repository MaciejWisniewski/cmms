using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class OnlyNotStartedNorFinishedServiceCanBeEditedRule : IBusinessRule
    {
        public DateTime? _actualStartDateTime { get; }
        public DateTime? _actualEndDateTime { get; }

        public OnlyNotStartedNorFinishedServiceCanBeEditedRule(DateTime? actualStartDateTime, DateTime? actualEndDateTime)
        {
            _actualStartDateTime = actualStartDateTime;
            _actualEndDateTime = actualEndDateTime;
        }

        public bool IsBroken() => _actualStartDateTime != null || _actualEndDateTime != null;

        public string Message => "Already started or finished services cannot be edited";
    }
}
