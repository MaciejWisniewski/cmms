using CMMS.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace CMMS.Application.Maintenance.Services.GetServicesInTimeRangeByResourceId
{
    public class GetServicesInTimeRangeByResourceIdQuery : IQuery<List<GetServicesInTimeRangeByResourceIdDto>>
    {
        public Guid ResourceId { get; }
        public DateTime From { get; }
        public DateTime To { get; }

        public GetServicesInTimeRangeByResourceIdQuery(Guid resourceId, DateTime from, DateTime to)
        {
            ResourceId = resourceId;
            From = from;
            To = to;
        }
    }
}
