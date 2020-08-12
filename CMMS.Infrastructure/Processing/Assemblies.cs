using CMMS.Application.Maintenance.Failures.RegisterFailure;
using System.Reflection;

namespace CMMS.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(RegisterFailureCommand).Assembly;
    }
}
