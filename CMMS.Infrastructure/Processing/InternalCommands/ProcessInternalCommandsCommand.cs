using CMMS.Application.Configuration.Commands;
using CMMS.Infrastructure.Processing.Outbox;
using MediatR;

namespace CMMS.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}
