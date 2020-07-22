﻿using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.SeedWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Resources.CreateResource
{
    public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, Guid>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateResourceCommandHandler(IResourceRepository resourceRepository, IUnitOfWork unitOfWork)
        {
            _resourceRepository = resourceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = Resource.CreateNew(
                request.ParentId,
                request.Name,
                request.IsArea,
                request.IsMachine);

            await _resourceRepository.AddAsync(resource);

            await _unitOfWork.CommitAsync(cancellationToken);

            return resource.Id.Value;
        }
    }
}
