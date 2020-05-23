using Autofac;
using Autofac.Core;
using CMMS.Application.Configuration.DomainEvents;
using CMMS.Domain.SeedWork;
using CMMS.Infrastructure.Database;
using CMMS.Infrastructure.Processing.Outbox;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Processing
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILifetimeScope _scope;
        private readonly MaintenanceContext _maintenanceContext;

        public DomainEventsDispatcher(IMediator mediator, ILifetimeScope scope, MaintenanceContext maintenanceContext)
        {
            this._mediator = mediator;
            this._scope = scope;
            this._maintenanceContext = maintenanceContext;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = this._maintenanceContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
            foreach (var domainEvent in domainEvents)
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                }
            }

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);

            foreach (var domainEventNotification in domainEventNotifications)
            {
                string type = domainEventNotification.GetType().FullName;
                var data = JsonConvert.SerializeObject(domainEventNotification);
                OutboxMessage outboxMessage = new OutboxMessage(
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);
                this._maintenanceContext.OutboxMessages.Add(outboxMessage);
            }
        }

        Task IDomainEventsDispatcher.DispatchEventsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
