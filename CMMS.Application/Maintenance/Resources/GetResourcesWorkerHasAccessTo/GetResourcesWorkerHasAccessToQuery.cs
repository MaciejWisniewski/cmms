using CMMS.Application.Configuration.Queries;
using CMMS.Application.Maintenance.Resources.GetAllResources;
using System;
using System.Collections.Generic;

namespace CMMS.Application.Maintenance.Resources.GetResourcesWorkerHasAccessTo
{
    public class GetResourcesWorkerHasAccessToQuery : IQuery<List<ResourceDto>>
    {
        public Guid WorkerId { get; }

        public GetResourcesWorkerHasAccessToQuery(Guid workerId)
        {
            WorkerId = workerId;
        }
    }
}
