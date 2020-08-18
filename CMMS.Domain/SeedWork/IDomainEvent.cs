using MediatR;
using System;

namespace CMMS.Domain.SeedWork
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
