using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Maintenance.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Resources.EditResource
{
    public class EditResourceCommandHandler : ICommandHandler<EditResourceCommand>
    {
        private readonly IResourceRepository _resourceRepository;

        public EditResourceCommandHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<Unit> Handle(EditResourceCommand command, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetByIdAsync(new ResourceId(command.ResourceId));
            if (resource == null)
                throw new NotFoundException("Resource with the given id hasn't been found", null);

            var parentResource = command.ParentId.HasValue ?
                await _resourceRepository.GetByIdAsync(new ResourceId(command.ParentId.Value)) :
                null;
            if (command.ParentId.HasValue && parentResource == null)
                throw new NotFoundException("Resource with the given parentId hasn't been found", null);

            resource.Edit(parentResource, command.Name);

            return Unit.Value;
        }
    }
}
