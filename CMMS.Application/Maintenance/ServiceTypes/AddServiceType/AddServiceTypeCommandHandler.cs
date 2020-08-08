using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Maintenance.ServiceTypes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.ServiceTypes.AddServiceType
{
    public class AddServiceTypeCommandHandler : ICommandHandler<AddServiceTypeCommand, Guid>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IServiceTypeUniquenessChecker _serviceTypeUniquenessChecker;

        public AddServiceTypeCommandHandler(
            IServiceTypeRepository serviceTypeRepository,
            IServiceTypeUniquenessChecker serviceTypeUniquenessChecker)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _serviceTypeUniquenessChecker = serviceTypeUniquenessChecker;
        }

        public async Task<Guid> Handle(AddServiceTypeCommand command, CancellationToken cancellationToken)
        {
            var serviceType = ServiceType.CreateNew(command.Name, _serviceTypeUniquenessChecker);

            await _serviceTypeRepository.AddAsync(serviceType);

            return serviceType.Id.Value;
        }
    }
}
