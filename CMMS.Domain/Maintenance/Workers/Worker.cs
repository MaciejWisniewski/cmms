using CMMS.Domain.Maintenance.Workers.Events;
using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Workers
{
    public class Worker : Entity, IAggregateRoot
    {
        public WorkerId Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }

        private Worker()
        {
        }

        private Worker(Guid id, string userName, string email, string fullName, string phoneNumber)
        {
            Id = new WorkerId(id);
            UserName = userName;
            Email = email;
            FullName = fullName;
            PhoneNumber = phoneNumber;

            AddDomainEvent(new WorkerCreatedDomainEvent(Id));
        }

        public static Worker Create(Guid id, string userName, string email, string fullName, string phoneNumber)
        {
            return new Worker(id, userName, email, fullName, phoneNumber);
        }

        public void Update(string fullName, string email, string phoneNumber)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;

            AddDomainEvent(new WorkerUpdatedDomainEvent(Id));
        }

        public void Remove(Action<Worker> removeMethod)
        {
            removeMethod(this);

            AddDomainEvent(new WorkerRemovedDomainEvent(Id));
        }
    }
}
