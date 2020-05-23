using MediatR;
using System.Threading.Tasks;

namespace CMMS.Application.Configuration.Processing
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(IRequest command);
    }
}
