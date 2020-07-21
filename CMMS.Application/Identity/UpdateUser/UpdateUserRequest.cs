using System;

namespace CMMS.Application.Identity.UpdateUser
{
    public class UpdateUserRequest
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string RoleName { get; set; }
    }
}
