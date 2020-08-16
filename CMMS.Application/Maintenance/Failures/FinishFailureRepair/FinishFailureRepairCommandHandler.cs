using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Maintenance.Failures;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.FinishFailureRepair
{
    public class FinishFailureRepairCommandHandler : ICommandHandler<FinishFailureRepairCommand>
    {
        private readonly IFailureRepository _failureRepository;
        private readonly IWorkerRepository _workerRepository;

        public FinishFailureRepairCommandHandler(IFailureRepository failureRepository, IWorkerRepository workerRepository)
        {
            _failureRepository = failureRepository;
            _workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(FinishFailureRepairCommand command, CancellationToken cancellationToken)
        {
            var failure = await _failureRepository.GetByIdAsync(new FailureId(command.FailureId));
            if (failure == null)
                throw new NotFoundException("Failure with the given id hasn't been found", null);

            var worker = await _workerRepository.GetByIdAsync(new WorkerId(command.WorkerId));
            if (worker == null)
                throw new NotFoundException("Worker with the given id hasn't been found", null);

            failure.FinishRepair(worker, command.Note);

            return Unit.Value;
        }
    }
}
