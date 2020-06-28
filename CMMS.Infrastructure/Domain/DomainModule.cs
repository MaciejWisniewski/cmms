using Autofac;
using CMMS.Application.Identity.DomainServices;
using CMMS.Domain.Identity;

namespace CMMS.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserUniquenessChecker>()
                .As<IUserUniquenessChecker>()
                .InstancePerLifetimeScope();
        }
    }
}
