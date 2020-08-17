using Autofac;
using CMMS.Application.Configuration.Data;
using CMMS.Domain.Identity;
using CMMS.Domain.Maintenance.Failures;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Services;
using CMMS.Domain.Maintenance.ServiceTypes;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;
using CMMS.Infrastructure.Domain;
using CMMS.Infrastructure.Domain.Identity;
using CMMS.Infrastructure.Domain.Maintenance.Failures;
using CMMS.Infrastructure.Domain.Maintenance.Resources;
using CMMS.Infrastructure.Domain.Maintenance.Services;
using CMMS.Infrastructure.Domain.Maintenance.ServiceTypes;
using CMMS.Infrastructure.Domain.Maintenance.Workers;

namespace CMMS.Infrastructure.Database
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;

        public DataAccessModule(string databaseConnectionString)
        {
            this._databaseConnectionString = databaseConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RoleRepository>()
                .As<IRoleRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ResourceRepository>()
                .As<IResourceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WorkerRepository>()
                .As<IWorkerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FailureRepository>()
                .As<IFailureRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ServiceTypeRepository>()
                .As<IServiceTypeRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ServiceRepository>()
                .As<IServiceRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
