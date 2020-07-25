using System.Collections.Generic;

namespace CMMS.Domain.SeedWork
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();

        //Created only as a marker to identify IdentityFramework based classes as Entities. This cannot
        //be done by inheritance after Entity because in C# you can't inherit after two classes.
        //IEntity marker allows for adding DomainEvents to classes (see AppUser) that cannot inherit after Entity.
    }
}