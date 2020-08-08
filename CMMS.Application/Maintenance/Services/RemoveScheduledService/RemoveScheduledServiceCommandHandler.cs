using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Services;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Services.RemoveScheduledService
{
    public class RemoveScheduledServiceCommandHandler : ICommandHandler<RemoveScheduledServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IResourceRepository _resourceRepository;

        public RemoveScheduledServiceCommandHandler(IServiceRepository serviceRepository, IResourceRepository resourceRepository)
        {
            _serviceRepository = serviceRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<Unit> Handle(RemoveScheduledServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(new ServiceId(command.ServiceId));
            var resource = await _resourceRepository.GetByIdAsync(service.ResourceId);

            service.Remove(resource, new WorkerId(command.WorkerId), _serviceRepository.Remove);

            return Unit.Value;
        }
    }
}
