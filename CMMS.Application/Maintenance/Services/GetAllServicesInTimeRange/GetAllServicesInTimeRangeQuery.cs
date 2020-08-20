using CMMS.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace CMMS.Application.Maintenance.Services.GetAllServicesInTimeRange
{
    public class GetAllServicesInTimeRangeQuery : IQuery<List<GetAllServicesInTimeRangeDto>>
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public GetAllServicesInTimeRangeQuery(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }
}
