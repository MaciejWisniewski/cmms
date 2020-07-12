using System;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Processing
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
