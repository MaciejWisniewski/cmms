using CMMS.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace CMMS.Application.Maintenance.Workers.GetWorkersHavingAccessTo
{
    public class GetWorkersHavingAccessToQuery : IQuery<List<WorkerDto>>
    {
        public Guid ResourceId { get; }

        public GetWorkersHavingAccessToQuery(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
