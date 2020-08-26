using CMMS.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMMS.Application.Maintenance.Failures.GetFailureInProgressByWorkerId
{
    public class GetFailureInProgressByWorkerIdQuery : IQuery<FailureDto>
    {
        public Guid WorkerId { get; }

        public GetFailureInProgressByWorkerIdQuery(Guid workerId)
        {
            WorkerId = workerId;
        }
    }
}
