using CMMS.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace CMMS.Application.Maintenance.Failures.GetFailuresInTimeRangeByResourceId
{
    public class GetFailuresInTimeRangeByResourceIdQuery : IQuery<List<GetFailuresInTimeRangeByResourceIdDto>>
    {
        public Guid ResourceId { get; }
        public DateTime From { get; }
        public DateTime To { get; }

        public GetFailuresInTimeRangeByResourceIdQuery(Guid resourceId, DateTime from, DateTime to)
        {
            ResourceId = resourceId;
            From = from;
            To = to;
        }
    }
}
