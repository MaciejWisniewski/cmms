using System;

namespace CMMS.Application.Identity
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; }
    }
}
