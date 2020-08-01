using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Services.Rules
{
    public class ServiceScheduledStartMustBeBeforeItsScheduledEndRule : IBusinessRule
    {
        private readonly DateTime _scheduledStartDateTime;
        private readonly DateTime _scheduledEndDateTime;

        public ServiceScheduledStartMustBeBeforeItsScheduledEndRule(
            DateTime scheduledStartDateTime,
            DateTime scheduledEndDateTime)
        {
            _scheduledStartDateTime = scheduledStartDateTime;
            _scheduledEndDateTime = scheduledEndDateTime;
        }

        public bool IsBroken() => _scheduledStartDateTime.CompareTo(_scheduledEndDateTime) >= 0;

        public string Message => "Scheduled service must start before it ends";
    }
}
