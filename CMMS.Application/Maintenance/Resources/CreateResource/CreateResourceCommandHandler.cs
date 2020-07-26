using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Maintenance.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Resources.CreateResource
{
    public class CreateResourceCommandHandler : ICommandHandler<CreateResourceCommand, Guid>
    {
        private readonly IResourceRepository _resourceRepository;

        public CreateResourceCommandHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<Guid> Handle(CreateResourceCommand command, CancellationToken cancellationToken)
        {
            var parentResource = command.ParentId.HasValue ?
                await _resourceRepository.GetByIdAsync(new ResourceId(command.ParentId.Value)) :
                null;

            if (command.ParentId.HasValue && parentResource == null)
                throw new NotFoundException("Resource with the given parentId hasn't been found", null);

            var resource = Resource.CreateNew(
                parentResource,
                command.Name,
                command.IsArea,
                command.IsMachine);

            await _resourceRepository.AddAsync(resource);

            return resource.Id.Value;
        }
    }
}
