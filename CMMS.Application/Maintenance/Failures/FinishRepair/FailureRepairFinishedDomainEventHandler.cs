using CMMS.Domain.Failures;
using CMMS.Domain.Maintenance.Failures.Events;
using CMMS.Domain.Maintenance.Resources;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.FinishRepair
{
    public class FailureRepairFinishedDomainEventHandler : INotificationHandler<FailureRepairFinishedDomainEvent>
    {

        private readonly IHubContext<FailureHub> _hubContext;
        private readonly IResourceRepository _resourceRepository;

        public FailureRepairFinishedDomainEventHandler(IHubContext<FailureHub> hubContext, IResourceRepository resourceRepository)
        {
            _hubContext = hubContext;
            _resourceRepository = resourceRepository;
        }

        public async Task Handle(FailureRepairFinishedDomainEvent notification, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetByIdAsync(notification.ResourceId);
            await _hubContext.Clients.All.SendAsync("notifyFailureRepairFinished", new FailureDto()
            {
                Id = notification.FailureId.Value,
                ResourceId = notification.ResourceId.Value,
                ResourceName = resource.Name,
                WorkerId = notification.WorkerId.Value,
                WorkerUserName = notification.WorkerUserName,
                State = notification.FailureState.Value,
                ProblemDescription = notification.ProblemDescription,
                Note = notification.Note,
                OccurredOn = notification.OccurredOn,
                ResolvedOn = notification.ResolvedOn
            });
        }
    }
}
