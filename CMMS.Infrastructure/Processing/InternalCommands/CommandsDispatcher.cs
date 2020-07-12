using CMMS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Processing.InternalCommands
{
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly MaintenanceContext _maintenanceContext;

        public CommandsDispatcher(
            IMediator mediator,
            MaintenanceContext maintenanceContext)
        {
            _mediator = mediator;
            _maintenanceContext = maintenanceContext;
        }

        public async Task DispatchCommandAsync(Guid id)
        {
            var command = await this._maintenanceContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            Type type = Assembly.GetAssembly(typeof(IRequest<>)).GetType(command.Type);
            var request = JsonConvert.DeserializeObject(command.Data, type);

            command.ProcessedDate = DateTime.UtcNow;

            await this._mediator.Send((IRequest)request);
        }
    }
}
