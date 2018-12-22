using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Notifications.API.Models
{
    public class Email
    {
        public List<string> To { get; set; } = new List<string>();
        public List<string> Cc { get; set; } = new List<string>();
        public List<string> Bcc { get; set; } = new List<string>();
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
