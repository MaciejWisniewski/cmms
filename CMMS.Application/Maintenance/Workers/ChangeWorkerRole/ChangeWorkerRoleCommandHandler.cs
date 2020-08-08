using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Workers.ChangeWorkerRole
{
    public class ChangeWorkerRoleCommandHandler : ICommandHandler<ChangeWorkerRoleCommand>
    {
        private readonly IWorkerRepository _workerRepository;

        public ChangeWorkerRoleCommandHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(ChangeWorkerRoleCommand command, CancellationToken cancellationToken)
        {
            var worker = await _workerRepository.GetByIdAsync(new WorkerId(command.WorkerId));

            worker.ChangeRole(command.Role);

            return Unit.Value;
        }
    }
}
