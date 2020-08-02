using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Services.RemoveScheduledService
{
    public class RemoveScheduledServiceCommandHandler : ICommandHandler<RemoveScheduledServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;

        public RemoveScheduledServiceCommandHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<Unit> Handle(RemoveScheduledServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(new ServiceId(command.ServiceId));

            service.Remove(_serviceRepository.Remove);

            return Unit.Value;
        }
    }
}
