﻿using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Workers.UpdateWorker
{
    public class UpdateWorkerCommand : CommandBase
    {
        public Guid WorkerId { get; }
        public string FullName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string Role { get; }

        public UpdateWorkerCommand(Guid workerId, string fullName, string email, string phoneNumber, string role)
        {
            WorkerId = workerId;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
        }
    }
}
