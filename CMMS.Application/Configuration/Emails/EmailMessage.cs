﻿namespace CMMS.Application.Configuration.Emails
{
    public struct EmailMessage
    {
        public string From { get; }
        public string To { get; }
        public string Subject { get; }
        public string Content { get; }

        public EmailMessage(
            string from,
            string to,
            string subject,
            string content)
        {
            From = from;
            To = to;
            Subject = subject;
            Content = content;
        }
    }
}
