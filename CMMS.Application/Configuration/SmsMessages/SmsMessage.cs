namespace CMMS.Application.Configuration.SmsMessages
{
    public class SmsMessage
    {
        public string From { get; }
        public string To { get; }
        public string Content { get; }

        public SmsMessage(
            string from,
            string to,
            string content)
        {
            From = from;
            To = to;
            Content = content;
        }
    }
}
