using Microsoft.AspNetCore.Identity;

namespace CMMS.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}
