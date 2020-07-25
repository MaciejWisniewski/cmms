using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.SeedWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Resources.EditResource
{
    public class EditResourceCommandHandler : IRequestHandler<EditResourceCommand>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditResourceCommandHandler(IResourceRepository resourceRepository, IUnitOfWork unitOfWork)
        {
            _resourceRepository = resourceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(EditResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.GetByIdAsync(new ResourceId(request.ResourceId));
            if (resource == null)
                throw new NotFoundException("Resource with the given id hasn't been found", null);

            var parentResource = request.ParentId.HasValue ?
                await _resourceRepository.GetByIdAsync(new ResourceId(request.ParentId.Value)) :
                null;
            if (request.ParentId.HasValue && parentResource == null)
                throw new NotFoundException("Resource with the given parentId hasn't been found", null);

            resource.Edit(parentResource, request.Name);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
