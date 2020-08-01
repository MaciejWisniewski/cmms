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

        public FinishServiceCommandHandler(IServiceRepository serviceRepository, IResourceRepository resourceRepository)
        {
            _serviceRepository = serviceRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<Unit> Handle(FinishServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(new ServiceId(command.ServiceId));
            var resource = await _resourceRepository.GetByIdAsync(service.ResourceId);

            service.Finish(resource, new WorkerId(command.FinishingWorkerId), command.Note);

            return Unit.Value;
        }
    }
}
