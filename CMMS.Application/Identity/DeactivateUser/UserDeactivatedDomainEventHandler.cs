using CMMS.Application.Maintenance.Workers.RemoveWorker;
using CMMS.Domain.Identity.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.DeactivateUser
{
    public class UserDeactivatedDomainEventHandler : INotificationHandler<UserDeactivatedDomainEvent>
    {
        private readonly IMediator _mediator;

        public UserDeactivatedDomainEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserDeactivatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RemoveWorkerCommand(notification.UserId));
        }
    }
}
