using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Workers.RemoveWorker
{
    public class RemoveWorkerCommandHandler : ICommandHandler<RemoveWorkerCommand>
    {
        private readonly IWorkerRepository _workerRepository;

        public RemoveWorkerCommandHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(RemoveWorkerCommand command, CancellationToken cancellationToken)
        {
            var worker = await _workerRepository.GetByIdAsync(new WorkerId(command.WorkerId));

            worker?.Remove(_workerRepository.Remove);

            return Unit.Value;
        }
    }
}
