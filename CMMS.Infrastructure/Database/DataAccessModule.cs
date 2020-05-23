using Autofac;
using CMMS.Application.Configuration.Data;

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

            //builder.RegisterType<UnitOfWork>()
            //    .As<IUnitOfWork>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<AreaRepository>()
            //    .As<IProductRepository>()
            //    .InstancePerLifetimeScope();


            //builder.RegisterType<StronglyTypedIdValueConverterSelector>()
            //    .As<IValueConverterSelector>()
            //    .InstancePerLifetimeScope();
        }
    }
}
