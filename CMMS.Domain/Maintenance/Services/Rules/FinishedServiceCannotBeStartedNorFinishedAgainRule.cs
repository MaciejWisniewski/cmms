using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class FinishedServiceCannotBeStartedNorFinishedAgainRule : IBusinessRule
    {
        private readonly DateTime? _actualEndDateTime;

        public FinishedServiceCannotBeStartedNorFinishedAgainRule(DateTime? actualEndDateTime)
        {
            _actualEndDateTime = actualEndDateTime;
        }

        public bool IsBroken() => _actualEndDateTime != null;

        public string Message => "Service has already been finished";
    }
}
