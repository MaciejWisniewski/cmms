using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Services;
using CMMS.Domain.Maintenance.ServiceTypes;
using CMMS.Domain.Maintenance.Workers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Services.EditScheduledService
{
    public class EditScheduledServiceCommandHandler : ICommandHandler<EditScheduledServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IResourceRepository _resourceRepository;

        public EditScheduledServiceCommandHandler(IServiceRepository serviceRepository, IResourceRepository resourceRepository)
        {
            _serviceRepository = serviceRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<Unit> Handle(EditScheduledServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(new ServiceId(command.ServiceId));
            var resource = await _resourceRepository.GetByIdAsync(new ResourceId(command.ResourceId));

            service.Edit(
                resource,
                new ServiceTypeId(command.ServiceTypeId),
                new WorkerId(command.ScheduledWorkerId),
                command.Description,
                command.ScheduledStartDateTime,
                command.ScheduledEndDateTime);

            return Unit.Value;
        }
    }
}
