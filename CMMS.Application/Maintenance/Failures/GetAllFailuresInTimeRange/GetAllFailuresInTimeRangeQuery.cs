using CMMS.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace CMMS.Application.Maintenance.Failures.GetAllFailuresInTimeRange
{
    public class GetAllFailuresInTimeRangeQuery : IQuery<List<GetAllFailuresInTimeRangeDto>>
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public GetAllFailuresInTimeRangeQuery(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }
}
