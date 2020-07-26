using CMMS.Application.Configuration.Commands;
using CMMS.Domain.SeedWork;
using CMMS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Processing
{
    public class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;

        private readonly IUnitOfWork _unitOfWork;

        private readonly MaintenanceContext _maintenanceContext;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<T> decorated,
            IUnitOfWork unitOfWork,
            MaintenanceContext ordersContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _maintenanceContext = ordersContext;
        }

        public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            await this._decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase)
            {
                var internalCommand =
                    await _maintenanceContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id,
                        cancellationToken: cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await this._unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
