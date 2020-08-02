using CMMS.Application.Maintenance.Workers.UpdateWorker;
using CMMS.Domain.Identity.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.UpdateUser
{
    public class UserUpdatedDomainEventHandler : INotificationHandler<UserUpdatedDomainEvent>
    {
        private readonly IMediator _mediator;

        public UserUpdatedDomainEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateWorkerCommand(
                    notification.UserId,
                    notification.FullName,
                    notification.Email,
                    notification.PhoneNumber,
                    notification.RoleName
                ));
        }
    }
}
