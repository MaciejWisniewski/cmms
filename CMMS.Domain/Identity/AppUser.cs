using CMMS.Domain.Identity.Events;
using CMMS.Domain.Identity.Rules;
using CMMS.Domain.SeedWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CMMS.Domain.Identity
{
    public class AppUser : IdentityUser<Guid>, IAggregateRoot, IEntity
    {
        private List<IDomainEvent> _domainEvents;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();
        public string FullName { get; set; }

        public AppUser()
        {
            Id = Guid.NewGuid();
        }

        private void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public static AppUser Create(
            string fullName, 
            string userName, 
            string email, 
            string phoneNumber,
            IUserUniquenessChecker userUniquenessChecker)
        {
            CheckRule(new UserMustHaveUniqueUsernameAndEmail(userUniquenessChecker, userName, email));

            return new AppUser(fullName, userName, email, phoneNumber);
        }

        private AppUser(string fullName, string userName, string email, string phoneNumber)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;

            AddDomainEvent(new UserCreatedDomainEvent(Id, UserName, Email, FullName, PhoneNumber));
        }

        public void Update(string fullName, string email, string phoneNumber)
        {
            FullName = fullName;
            Email = email;
            NormalizedEmail = email.Normalize().ToUpperInvariant();
            PhoneNumber = phoneNumber;

            AddDomainEvent(new UserUpdatedDomainEvent(Id, FullName, Email, PhoneNumber));
        }

        public void ChangeRole(AppRole role)
        {
            AddDomainEvent(new ChangedUserRoleDomainEvent(Id, role.Name));
        }

        private static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
