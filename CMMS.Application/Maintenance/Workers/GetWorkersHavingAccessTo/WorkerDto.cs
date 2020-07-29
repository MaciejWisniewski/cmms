﻿using System;

namespace CMMS.Application.Maintenance.Workers.GetWorkersHavingAccessTo
{
    public class WorkerDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
