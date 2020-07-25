using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Resources.RemoveResource
{
    public class RemoveResourceCommandHandler : IRequestHandler<RemoveResourceCommand>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveResourceCommandHandler(IResourceRepository resourceRepository, IUnitOfWork unitOfWork)
        {
            _resourceRepository = resourceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetByIdAsync(new ResourceId(request.ResourceId));

            if (resource == null)
                throw new NotFoundException("Resource with the given id hasn't been found", null);

            resource.Remove(_resourceRepository.Remove);

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
