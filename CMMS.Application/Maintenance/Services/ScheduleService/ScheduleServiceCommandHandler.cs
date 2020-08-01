using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Services;
using CMMS.Domain.Maintenance.ServiceTypes;
using CMMS.Domain.Maintenance.Workers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Services.ScheduleService
{
    public class ScheduleServiceCommandHandler : ICommandHandler<ScheduleServiceCommand, Guid>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IResourceRepository _resourceRepository;

        public ScheduleServiceCommandHandler(
            IServiceRepository serviceRepository, 
            IResourceRepository resourceRepository)
        {
            _serviceRepository = serviceRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<Guid> Handle(ScheduleServiceCommand command, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetByIdAsync(new ResourceId(command.ResourceId));
            var service = Service.Schedule(
                    resource,
                    new ServiceTypeId(command.ServiceTypeId),
                    new WorkerId(command.ScheduledWorkerId),
                    command.Description,
                    command.ScheduledStartDateTime,
                    command.ScheduledEndDateTime
                );

            await _serviceRepository.AddAsync(service);

            return service.Id.Value;
        }
    }
}
