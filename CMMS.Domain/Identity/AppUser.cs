using CMMS.Domain.Identity.Events;
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
        public string FullName { get; private set; }

        public AppUser()
        {
            Id = Guid.NewGuid();
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
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
            var isUnique = userUniquenessChecker.IsUnique(userName, email);
            if (!isUnique)
                throw new BusinessRuleValidationException("User with the given username or email already exists");

            return new AppUser(fullName, userName, email, phoneNumber);
        }

        private AppUser(string fullName, string userName, string email, string phoneNumber)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;

            AddDomainEvent(new UserCreatedDomainEvent(Id));
        }

        public void Update(string fullName, string email, string phoneNumber)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;

            AddDomainEvent(new UserUpdatedDomainEvent(Id));
        }

        public void ChangeRole(AppRole role)
        {
            AddDomainEvent(new ChangedUserRoleDomainEvent(Id, role.Name));
        }
    }
}
