using Autofac;
using CMMS.Application.Identity.DomainServices;
using CMMS.Application.Maintenance.DomainServices;
using CMMS.Domain.Identity;
using CMMS.Domain.Maintenance.ServiceTypes;

namespace CMMS.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserUniquenessChecker>()
                .As<IUserUniquenessChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ServiceTypeUniquenessChecker>()
                .As<IServiceTypeUniquenessChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RoleValidator>()
                .As<IRoleValidator>()
                .InstancePerLifetimeScope();
        }
    }
}
