using CMMS.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMMS.Application.Maintenance.Services.GetServiceInProgressByWorkerId
{
    public class GetServiceInProgressByWorkerIdQuery : IQuery<GetServiceInProgressByWorkerIdDto>
    {
        public GetServiceInProgressByWorkerIdQuery(Guid workerId)
        {
            WorkerId = workerId;
        }
        public Guid WorkerId { get; }
    }
}
