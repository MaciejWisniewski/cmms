using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Services;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Services.StartService
{
    public class StartServiceCommandHandler : ICommandHandler<StartServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IResourceRepository _resourceRepository;

        public StartServiceCommandHandler(IServiceRepository serviceRepository, IResourceRepository resourceRepository)
        {
            _serviceRepository = serviceRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<Unit> Handle(StartServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(new ServiceId(command.ServiceId));
            var resource = await _resourceRepository.GetByIdAsync(service.ResourceId);

            service.Start(resource, new WorkerId(command.ActualWorkerId), command.Note);

            return Unit.Value;
        }
    }
}
