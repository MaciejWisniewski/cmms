using System;

namespace CMMS.Application.Maintenance.Services.FinishService
{
    public class FinishServiceRequest
    {
        public Guid FinishingWorkerId { get; set; }
        public string Note { get; set; }
    }
}
