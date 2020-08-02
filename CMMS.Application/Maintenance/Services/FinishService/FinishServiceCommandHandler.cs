using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Services;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Services.FinishService
{
    public class FinishServiceCommandHandler : ICommandHandler<FinishServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IWorkerRepository _workerRepository;

        public FinishServiceCommandHandler(
            IServiceRepository serviceRepository, 
            IResourceRepository resourceRepository,
            IWorkerRepository workerRepository)
        {
            _serviceRepository = serviceRepository;
            _resourceRepository = resourceRepository;
            _workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(FinishServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(new ServiceId(command.ServiceId));
            var resource = await _resourceRepository.GetByIdAsync(service.ResourceId);
            var finishingWorker = await _workerRepository.GetByIdAsync(new WorkerId(command.FinishingWorkerId));

            service.Finish(resource, finishingWorker, command.Note);

            return Unit.Value;
        }
    }
}
