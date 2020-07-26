using Autofac;
using CMMS.Application.Configuration.Data;
using CMMS.Domain.Identity;
using CMMS.Domain.Maintenance.Operators;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.SeedWork;
using CMMS.Infrastructure.Domain;
using CMMS.Infrastructure.Domain.Identity;
using CMMS.Infrastructure.Domain.Maintenance.Operators;
using CMMS.Infrastructure.Domain.Maintenance.Resources;

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

            builder.RegisterType<OperatorRepository>()
                .As<IOperatorRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
