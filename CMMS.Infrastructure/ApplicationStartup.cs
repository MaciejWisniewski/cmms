﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CMMS.Application.Configuration;
using CMMS.Application.Configuration.Emails;
using CMMS.Application.Configuration.SmsMessages;
using CMMS.Infrastructure.Database;
using CMMS.Infrastructure.Domain;
using CMMS.Infrastructure.Emails;
using CMMS.Infrastructure.Processing;
using CMMS.Infrastructure.Processing.InternalCommands;
using CMMS.Infrastructure.Processing.Outbox;
using CMMS.Infrastructure.Quartz;
using CMMS.Infrastructure.SeedWork;
using CMMS.Infrastructure.SmsMessages;
using CommonServiceLocator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;

namespace CMMS.Infrastructure
{
    public class ApplicationStartup
    {
        public static IServiceProvider Initialize(
            IServiceCollection services,
            string connectionString,
            IEmailSender emailSender,
            EmailsSettings emailsSettings,
            ISmsMessageSender smsMessageSender,
            SmsMessagesSettings smsMessagesSettings,
            IExecutionContextAccessor executionContextAccessor,
            bool runQuartz = true)
        {
            if (runQuartz)
            {
                StartQuartz(connectionString, emailsSettings, smsMessagesSettings, executionContextAccessor);
            }

            //services.AddSingleton(cacheStore);

            var serviceProvider = CreateAutofacServiceProvider(
                services,
                connectionString,
                emailSender,
                emailsSettings,
                smsMessageSender,
                smsMessagesSettings,
                executionContextAccessor);

            return serviceProvider;
        }

        private static IServiceProvider CreateAutofacServiceProvider(
            IServiceCollection services,
            string connectionString,
            IEmailSender emailSender,
            EmailsSettings emailsSettings,
            ISmsMessageSender smsMessageSender,
            SmsMessagesSettings smsMessagesSettings,
            IExecutionContextAccessor executionContextAccessor)
        {
            var container = new ContainerBuilder();

            container.Populate(services);

            //container.RegisterModule(new LoggingModule(logger));
            container.RegisterModule(new DataAccessModule(connectionString));
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new DomainModule());

            if (emailSender != null)
                container.RegisterModule(new EmailModule(emailSender, emailsSettings));
            else
                container.RegisterModule(new EmailModule(emailsSettings));

            if (smsMessageSender != null)
                container.RegisterModule(new SmsMessageModule(smsMessageSender, smsMessagesSettings));
            else
                container.RegisterModule(new SmsMessageModule(smsMessagesSettings));

            container.RegisterModule(new ProcessingModule());

            container.RegisterInstance(executionContextAccessor);

            var buildContainer = container.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));

            var serviceProvider = new AutofacServiceProvider(buildContainer);

            CompositionRoot.SetContainer(buildContainer);

            return serviceProvider;
        }

        private static void StartQuartz(
            string connectionString,
            EmailsSettings emailsSettings,
            SmsMessagesSettings smsMessagesSettings,
            IExecutionContextAccessor executionContextAccessor)
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            var container = new ContainerBuilder();

            container.RegisterModule(new QuartzModule());
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new DataAccessModule(connectionString));
            container.RegisterModule(new EmailModule(emailsSettings));
            container.RegisterModule(new SmsMessageModule(smsMessagesSettings));
            container.RegisterModule(new ProcessingModule());

            container.RegisterInstance(executionContextAccessor);
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
                    .WithCronSchedule("0/5 * * ? * *")
                    .Build();

            scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult();

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/5 * * ? * *")
                    .Build();
            scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();
        }
    }
}
