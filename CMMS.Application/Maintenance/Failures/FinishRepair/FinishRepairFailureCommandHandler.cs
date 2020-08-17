using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Maintenance.Failures;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.FinishRepair
{
    public class FinishRepairFailureCommandHandler : ICommandHandler<FinishRepairFailureCommand>
    {
        private readonly IFailureRepository _failureRepository;
        private readonly IWorkerRepository _workerRepository;

        public FinishRepairFailureCommandHandler(IFailureRepository failureRepository, IWorkerRepository workerRepository)
        {
            _failureRepository = failureRepository;
            _workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(FinishRepairFailureCommand request, CancellationToken cancellationToken)
        {
            var failure = await _failureRepository.GetByIdAsync(new FailureId(request.FailureId));
            if (failure == null)
                throw new NotFoundException("Failure with the given id hasn't been found", null);

            var worker = await _workerRepository.GetByIdAsync(new WorkerId(request.WorkerId));
            if (worker == null)
                throw new NotFoundException("Worker with the given id hasn't been found", null);
            failure.FinishRepair(worker);
            return Unit.Value;
        }
    }
}
