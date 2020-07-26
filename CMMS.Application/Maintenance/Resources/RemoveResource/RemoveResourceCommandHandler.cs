using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Maintenance.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Resources.RemoveResource
{
    public class RemoveResourceCommandHandler : ICommandHandler<RemoveResourceCommand>
    {
        private readonly IResourceRepository _resourceRepository;

        public RemoveResourceCommandHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<Unit> Handle(RemoveResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetByIdAsync(new ResourceId(request.ResourceId));

            if (resource == null)
                throw new NotFoundException("Resource with the given id hasn't been found", null);

            resource.Remove(_resourceRepository.Remove);

            return Unit.Value;
        }
    }
}
