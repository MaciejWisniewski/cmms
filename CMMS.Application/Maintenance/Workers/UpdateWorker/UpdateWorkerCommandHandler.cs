using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Workers.UpdateWorker
{
    public class UpdateWorkerCommandHandler : ICommandHandler<UpdateWorkerCommand>
    {
        private readonly IWorkerRepository _workerRepository;

        public UpdateWorkerCommandHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(UpdateWorkerCommand command, CancellationToken cancellationToken)
        {
            var worker = await _workerRepository.GetByIdAsync(new WorkerId(command.WorkerId));

            worker.Update(command.FullName, command.Email, command.PhoneNumber);

            return Unit.Value;
        }
    }
}
