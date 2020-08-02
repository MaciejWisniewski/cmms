using CMMS.Application.Maintenance.Workers.ChangeWorkerRole;
using CMMS.Domain.Identity.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.ChangeUserRole
{
    public class ChangedUserRoleDomainEventHandler : INotificationHandler<ChangedUserRoleDomainEvent>
    {
        private readonly IMediator _mediator;

        public ChangedUserRoleDomainEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(ChangedUserRoleDomainEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new ChangeWorkerRoleCommand(notification.UserId, notification.NewRoleName));   
        }
    }
}
