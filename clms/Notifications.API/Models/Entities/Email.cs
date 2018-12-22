using System;
using System.Collections.Generic;

namespace Notifications.API.Models.Entities
{
    public class Email
    {
        private Email()
        {
            //EF
        }

        public Email(string from, List<string> to, List<string> cc, List<string> bcc, string subject, string body)
        {
            Id = Guid.NewGuid();
            From = from;
            To = to;
            Cc = cc;
            Bcc = bcc;
            Subject = subject;
            Body = body;
        }

        public Guid Id { get; private set; }
        public string From { get; private set; }
        public List<string> To { get; private set; }
        public List<string> Cc { get; private set; }
        public List<string> Bcc { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
    }
}