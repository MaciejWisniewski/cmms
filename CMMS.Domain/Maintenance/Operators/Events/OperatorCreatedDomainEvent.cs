using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Operators.Events
{
    public class OperatorCreatedDomainEvent : DomainEventBase
    {
        public OperatorId OperatorId { get; }

        public OperatorCreatedDomainEvent(OperatorId operatorId)
        {
            OperatorId = operatorId;
        }
    }
}
