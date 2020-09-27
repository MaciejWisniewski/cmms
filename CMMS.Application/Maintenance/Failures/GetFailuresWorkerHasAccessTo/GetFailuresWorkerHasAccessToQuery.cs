using CMMS.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace CMMS.Application.Maintenance.Failures.GetFailuresWorkerHasAccessTo
{
    public class GetFailuresWorkerHasAccessToQuery : IQuery<List<FailureDto>>
    {
        public Guid WorkerId { get; }

        public GetFailuresWorkerHasAccessToQuery(Guid workerId)
        {
            WorkerId = workerId;
        }
    }
}
