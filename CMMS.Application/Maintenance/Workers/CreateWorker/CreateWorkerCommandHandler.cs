using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Workers.CreateWorker
{
    public class CreateWorkerCommandHandler : ICommandHandler<CreateWorkerCommand>
    {
        private readonly IWorkerRepository _workerRepository;

        public CreateWorkerCommandHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(CreateWorkerCommand command, CancellationToken cancellationToken)
        {
            var worker = Worker.Create(
                    command.WorkerId,
                    command.UserName,
                    command.Email,
                    command.FullName,
                    command.PhoneNumber
                );

            await _workerRepository.AddAsync(worker);

            return Unit.Value;
        }
    }
}
