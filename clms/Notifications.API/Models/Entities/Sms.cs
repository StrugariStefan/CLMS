using System;
using System.Collections.Generic;

namespace Notifications.API.Models.Entities
{
    public class Sms
    {
        private Sms()
        {
            //EF
        }

        public Sms(string from, List<string> to, string message)
        {
            Id = Guid.NewGuid();
            From = from;
            To = to;
            Message = message;
        }

        public Guid Id { get; private set; }
        public string From { get; private set; }
        public List<string> To { get; private set; }
        public string Message { get; private set; }
    }
}
