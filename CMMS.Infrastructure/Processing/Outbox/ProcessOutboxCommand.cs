using CMMS.Application.Configuration.Commands;
using MediatR;

namespace CMMS.Infrastructure.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand
    {
    }
}
