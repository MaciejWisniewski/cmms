using CMMS.Domain.SeedWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CMMS.Domain.Identity
{
    public class AppRole : IdentityRole<Guid>, IAggregateRoot
    {
        public AppRole(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => throw new NotImplementedException();

        public void ClearDomainEvents()
        {
            throw new NotImplementedException();
        }
    }
}
