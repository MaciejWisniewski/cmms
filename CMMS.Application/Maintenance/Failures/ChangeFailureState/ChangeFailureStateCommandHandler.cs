using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Maintenance.Failures;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.ChangeFailureState
{
    public class ChangeFailureStateCommandHandler : ICommandHandler<ChangeFailureStateCommand>
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IFailureRepository _failureReposiotry;

        public ChangeFailureStateCommandHandler(IWorkerRepository workerRepository, IFailureRepository failureReposiotry)
        {
            _workerRepository = workerRepository;
            _failureReposiotry = failureReposiotry;
        }

        public async Task<Unit> Handle(ChangeFailureStateCommand request, CancellationToken cancellationToken)
        {
            var failure = await _failureReposiotry.GetByIdAsync(new FailureId(request.FailureId));
            if (failure == null)
                throw new NotFoundException("Failure with the given id hasn't been found", null);

            var worker = await _workerRepository.GetByIdAsync(new WorkerId(request.WorkerId));
            if (worker == null)
                throw new NotFoundException("Worker with the given id hasn't been found", null);

            FailureState failurState;

            switch (request.FailureState)
            {
                case "Detected":
                    failurState = FailureState.Detected;
                    break;
                case "InProgress":
                    failurState = FailureState.InProgress;
                    break;
                case "Resolved":
                    failurState = FailureState.Resolved;
                    break;
                default:
                    throw new NotFoundException("FailureState with the given id hasn't been found", null);
            }

            failure.ChangeState(failurState, worker, request.Note);

            return Unit.Value;
        }
    }
}
