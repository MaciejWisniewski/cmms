using CMMS.Domain.SeedWork;
using Microsoft.AspNetCore.Identity;
using System;

namespace CMMS.Domain.Identity
{
    public class AppUser : IdentityUser<Guid>, IAggregateRoot
    {
        public string FullName { get; private set; }

        public AppUser()
        {
            Id = Guid.NewGuid();
        }

        public static AppUser Create(string fullName, string userName, string email, string phoneNumber)
        {
            return new AppUser()
            {
                FullName = fullName,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber
            };
        }
    }
}
