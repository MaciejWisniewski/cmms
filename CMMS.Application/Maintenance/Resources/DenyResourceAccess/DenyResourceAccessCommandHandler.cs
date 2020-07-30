using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Resources.DenyResourceAccess
{
    public class DenyResourceAccessCommandHandler : ICommandHandler<DenyResourceAccessCommand>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IWorkerRepository _workerRepository;

        public DenyResourceAccessCommandHandler(IResourceRepository resourceRepository, IWorkerRepository workerRepository)
        {
            _resourceRepository = resourceRepository;
            _workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(DenyResourceAccessCommand command, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetByIdWithAllDescendantsAndAncestorsAsync(new ResourceId(command.ResourceId));
            if (resource == null)
                throw new NotFoundException("Resource with the given id hasn't been found", null);

            var worker = await _workerRepository.GetByIdAsync(new WorkerId(command.WorkerId));
            if (worker == null)
                throw new NotFoundException("Worker with the given id hasn't been found", null);

            resource.DenyAccess(worker.Id);

            return Unit.Value;
        }
    }
}
