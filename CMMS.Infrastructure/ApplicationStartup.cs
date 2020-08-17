﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CMMS.Infrastructure.Caching;
using CMMS.Infrastructure.Database;
using CMMS.Infrastructure.Domain;
using CMMS.Infrastructure.Processing;
using CMMS.Infrastructure.Processing.InternalCommands;
using CMMS.Infrastructure.Processing.Outbox;
using CMMS.Infrastructure.Quartz;
using CMMS.Infrastructure.SeedWork;
using CommonServiceLocator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;

namespace CMMS.Infrastructure
{
    public class ApplicationStartup
    {
        public static IServiceProvider Initialize(
            IServiceCollection services,
            string connectionString,
            Dictionary<string, TimeSpan> cachingConfiguration)
        {
            StartQuartz(connectionString);

            var serviceProvider = CreateAutofacServiceProvider(services, connectionString, cachingConfiguration);
            SeedDatabase.Initialize(serviceProvider);

            return serviceProvider;
        }

        private static IServiceProvider CreateAutofacServiceProvider(
            IServiceCollection services,
            string connectionString,
            Dictionary<string, TimeSpan> cachingConfiguration)
        {
            var container = new ContainerBuilder();

            container.Populate(services);

            container.RegisterModule(new DataAccessModule(connectionString));
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new DomainModule());
            //container.RegisterModule(new EmailModule());
            container.RegisterModule(new ProcessingModule());

            container.RegisterModule(new CachingModule(cachingConfiguration));

            var buildContainer = container.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));

            var serviceProvider = new AutofacServiceProvider(buildContainer);

            return serviceProvider;
        }

        private static void StartQuartz(string connectionString)
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            var container = new ContainerBuilder();
            container.RegisterModule(new QuartzModule());
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new DataAccessModule(connectionString));
            //container.RegisterModule(new EmailModule());
            container.RegisterModule(new ProcessingModule());

            container.Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<MaintenanceContext>();
                dbContextOptionsBuilder.UseSqlServer(connectionString);

                dbContextOptionsBuilder
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                return new MaintenanceContext(dbContextOptionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            scheduler.JobFactory = new JobFactory(container.Build());

            scheduler.Start().GetAwaiter().GetResult();

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();

            scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult();

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();
            scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();
        }
    }
}
