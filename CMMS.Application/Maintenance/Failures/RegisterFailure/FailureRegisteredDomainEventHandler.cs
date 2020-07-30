using CMMS.Domain.Failures;
using CMMS.Domain.Maintenance.Failures.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.RegisterFailure
{
    public class FailureRegisteredDomainEventHandler : INotificationHandler<FailureRegisteredDomainEvent>
    {
        private readonly IHubContext<FailureHub> _hubContext;

        public FailureRegisteredDomainEventHandler(IHubContext<FailureHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(FailureRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("notifyFailure", new FailureDto() { 
                Id = notification.FailureId.Value,
                ResourceId = notification.ResourceId.Value,
                State = notification.FailureState.Value,
                ProblemDescription = notification.ProblemDescription,
                OccuredOn = notification.OccuredOn
            });
        }
    }
}
