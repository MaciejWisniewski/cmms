namespace CMMS.Application.Identity.CreateUser
{
    public class CreateUserRequest
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}
