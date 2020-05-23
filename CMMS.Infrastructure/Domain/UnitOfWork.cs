using CMMS.Domain.SeedWork;
using CMMS.Infrastructure.Database;
using CMMS.Infrastructure.Processing;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MaintenanceContext _maintenanceContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            MaintenanceContext maintenanceContext,
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            _maintenanceContext = maintenanceContext;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await _maintenanceContext.SaveChangesAsync(cancellationToken);
        }
    }
}
