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

        public static AppUser Create(
            string fullName, 
            string userName, 
            string email, 
            string phoneNumber,
            IUserUniquenessChecker userUniquenessChecker)
        {
            var isUnique = userUniquenessChecker.IsUnique(userName, email);
            if (!isUnique)
                throw new BusinessRuleValidationException("User with the given username or email already exists");

            return new AppUser()
            {
                FullName = fullName,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber
            };
        }

        public void Update(string fullName, string email, string phoneNumber)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;       
        }
    }
}
