using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Maintenance.Failures;
using CMMS.Domain.Maintenance.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.RegisterFailure
{
    public class RegisterFailureCommandHandler : ICommandHandler<RegisterFailureCommand, Guid>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IFailureRepository _failureRepository;

        public RegisterFailureCommandHandler(IResourceRepository resourceRepository, IFailureRepository failureRepository)
        {
            _resourceRepository = resourceRepository;
            _failureRepository = failureRepository;
        }

        public async Task<Guid> Handle(RegisterFailureCommand command, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetByIdAsync(new ResourceId(command.ResourceId));
            if (resource == null)
                throw new NotFoundException("Resource with the given id hasn't been found", null);

            var failure = Failure.CreateNew(resource, command.ProblemDescription);

            await _failureRepository.AddAsync(failure);

            return failure.Id.Value;
        }
    }
}
