using Autofac;
using CMMS.Application.Configuration.SmsMessages;
using Module = Autofac.Module;

namespace CMMS.Infrastructure.SmsMessages
{
    internal class SmsMessageModule : Module
    {
        private readonly ISmsMessageSender _smsMessageSender;
        private readonly SmsMessagesSettings _smsMessageSettings;

        internal SmsMessageModule(ISmsMessageSender smsMessageSender, SmsMessagesSettings smsMessageSettings)
        {
            _smsMessageSender = smsMessageSender;
            _smsMessageSettings = smsMessageSettings;
        }

        internal SmsMessageModule(SmsMessagesSettings smsMessageSettings)
        {
            _smsMessageSettings = smsMessageSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_smsMessageSender != null)
            {
                builder.RegisterInstance(_smsMessageSender);
            }
            else
            {
                builder.RegisterType<SmsMessageSender>()
                    .As<ISmsMessageSender>()
                    .InstancePerLifetimeScope();
            }

            builder.RegisterInstance(_smsMessageSettings);
        }
    }
}
