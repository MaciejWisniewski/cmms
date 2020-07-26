using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Workers.CreateWorker
{
    public class CreateWorkerCommand : CommandBase
    {
        public Guid WorkerId { get; }
        public string UserName { get; }
        public string Email { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }

        public CreateWorkerCommand(Guid workerId, string userName, string email, string fullName, string phoneNumber)
        {
            WorkerId = workerId;
            UserName = userName;
            Email = email;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
    }
}
