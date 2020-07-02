using System;

namespace CMMS.Application.Configuration.Validation
{
    public class NotFoundException : Exception
    {
        public string Details { get; }

        public NotFoundException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}
