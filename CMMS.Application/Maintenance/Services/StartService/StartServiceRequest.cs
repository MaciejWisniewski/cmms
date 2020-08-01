using System;

namespace CMMS.Application.Maintenance.Services.StartService
{
    public class StartServiceRequest
    {
        public Guid ActualWorkerId { get; set; }
        public string Note { get; set; }
    }
}
