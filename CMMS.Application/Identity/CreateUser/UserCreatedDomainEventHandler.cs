using CMMS.Application.Maintenance.Workers.CreateWorker;
using CMMS.Domain.Identity.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.CreateUser
{
    public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IMediator _mediator;

        public UserCreatedDomainEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateWorkerCommand(
                    notification.UserId,
                    notification.UserName,
                    notification.Email,
                    notification.FullName,
                    notification.PhoneNumber,
                    notification.RoleName
                ));
        }
    }
}
