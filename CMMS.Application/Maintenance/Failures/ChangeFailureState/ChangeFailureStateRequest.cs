using System;

namespace CMMS.Application.Maintenance.Failures.ChangeFailureState
{
    public class ChangeFailureStateRequest
    {
        public Guid WorkerId { get; set; }
        public string Note { get; set; }
        public string FailureState { get; set; }
    }
}
