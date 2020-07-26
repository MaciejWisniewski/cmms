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
        public bool IsActive { get; private set; }

        public AppUser()
        {
            Id = Guid.NewGuid();
            IsActive = true;
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
            string roleName,
            IUserUniquenessChecker userUniquenessChecker)
        {
            CheckRule(new UserMustHaveUniqueUsernameAndEmail(userUniquenessChecker, userName, email));
            CheckRule(new CannotSetAnAdminRole(roleName));

            return new AppUser(fullName, userName, email, phoneNumber);
        }

        private AppUser(string fullName, string userName, string email, string phoneNumber)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            IsActive = true;

            AddDomainEvent(new UserCreatedDomainEvent(Id, UserName, Email, FullName, PhoneNumber));
        }

        public void Update(string fullName, string email, string phoneNumber, string roleName)
        {
            CheckRule(new CannotSetAnAdminRole(roleName));

            FullName = fullName;
            Email = email;
            NormalizedEmail = email.Normalize().ToUpperInvariant();
            PhoneNumber = phoneNumber;

            AddDomainEvent(new UserUpdatedDomainEvent(Id, FullName, Email, PhoneNumber));
        }

        public void ChangeRole(AppRole role)
        {
            CheckRule(new CannotSetAnAdminRole(role.Name));

            AddDomainEvent(new ChangedUserRoleDomainEvent(Id, role.Name));
        }

        public void Deactivate()
        {
            CheckRule(new CannotDeactivateAlreadyInactiveUser(this));

            IsActive = false;

            AddDomainEvent(new UserDeactivatedDomainEvent(Id));
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
