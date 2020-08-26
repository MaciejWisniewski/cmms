using CMMS.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace CMMS.Application.Maintenance.Failures.GetFailuresWorkerHasAccessTo
{
    public class GetFailureInProgressByWorkerId : IQuery<List<FailureDto>>
    {
        public Guid WorkerId { get; }

        public GetFailureInProgressByWorkerId(Guid workerId)
        {
            WorkerId = workerId;
        }
    }
}
