using CMMS.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace CMMS.Application.Maintenance.Services.GetServicesByWorkerAccesses
{
    public class GetServicesByWorkerAccessesQuery : IQuery<List<ServiceDto>>
    {
        public GetServicesByWorkerAccessesQuery(Guid workerId)
        {
            WorkerId = workerId;
        }

        public Guid WorkerId { get; }
    }
}
